const BASE_URL = "https://localhost:7045/api/todo";

// used to get all todo task with specified filters
function getAllTodos() {
  const params = new URLSearchParams();

  if (document.getElementById("sortBy").value != "")
    params.append("orderBy", document.getElementById("sortBy").value);

  if (document.getElementById("filterStatus").value != "")
    params.append("status", document.getElementById("filterStatus").value);

  if (document.getElementById("filterPriority").value != "")
    params.append("priority", document.getElementById("filterPriority").value);

  if (document.getElementById("startDate").value != "")
    params.append("startDate", document.getElementById("startDate").value);

  if (document.getElementById("endDate").value != "")
    params.append("endDate", document.getElementById("endDate").value);

  console.log(params);

  return fetch(`${BASE_URL}?${params}`)
    .then((response) => response.json())
    .then((data) => renderTodos(data));
}

// used to add event listener to "Status" , "Priority" , "Due Date" , "Sort By"
// when any of them get changed they call 'getAllTodos' to retrieve todos
// with specified filters
function addFilterListeners() {
  const filterControls = [
    "filterStatus",
    "filterPriority",
    "startDate",
    "endDate",
    "sortBy",
  ];

  filterControls.forEach((id) => {
    document.getElementById(id).addEventListener("change", () => {
      getAllTodos();
    });
  });
}

// render the todos on the screen
function renderTodos(todos) {
  const container = document.getElementById("todoContainer");
  container.innerHTML = "";

  todos.forEach((todo) => {
    const statusClass = `status-${todo.status.toLowerCase()}`;
    const priorityClass = `priority-${todo.priority.toLowerCase()}`;

    const todoCard = document.createElement("div");
    todoCard.className = `col-12 col-md-6 col-lg-4 mb-4`;
    todoCard.innerHTML = `
          <div class="todo-card card h-100 ${statusClass}">
              <div class="card-body">
                  <div class="action-buttons">
                ${
                  todo.status !== "Completed"
                    ? `
                <button class="btn btn-outline-success btn-sm complete-btn" data-todo-id="${todo.id}">
                    <i class="bi bi-check-lg"></i>
                </button>`
                    : ""
                }
                <button class="btn btn-outline-primary btn-sm edit-btn" data-todo-id="${
                  todo.id
                }">
                    <i class="bi bi-pencil"></i>
                </button>
                <button class="btn btn-outline-danger btn-sm delete-btn" data-todo-id="${
                  todo.id
                }">
                    <i class="bi bi-trash"></i>
                </button>
            </div>
                  <div class="d-flex align-items-start gap-3 mb-3">
                      <div class="${priorityClass} priority-badge">${
      todo.priority
    }</div>
                      <div class="status-badge status-badge-${todo.status.toLowerCase()}">
                          ${todo.status}
                      </div>
                      
                  </div>
                  
                  <h5 class="todo-title mb-3">${todo.title}</h5>
                  
                  ${
                    todo.description
                      ? `<p class="todo-description mb-3">${todo.description}</p>`
                      : ""
                  }
                  
                  <div class="due-date">
                      <i class="bi bi-calendar"></i>
                      <span>${
                        todo.dueDate ? formatDate(todo.dueDate) : "No due date"
                      }</span>
                  </div>
              </div>
          </div>
      `;

    container.appendChild(todoCard);
  });

  addTodoButtonsEventListeners();
}

// each todo have 3 button
// 1 -> to mark a todo task as completed
// 2 -> to edit a todo task
// 3 -> to delete todo task
// this method used to add event listener to each button
function addTodoButtonsEventListeners() {
  // handle delete of the todo task
  document.querySelectorAll(".delete-btn").forEach((btn) => {
    btn.addEventListener("click", () => {
      const todoId = btn.dataset.todoId;

      if (confirm("Are you sure you want to delete this todo?")) {
        fetch(`${BASE_URL}/${todoId}`, { method: "DELETE" }).then(() =>
          getAllTodos()
        );
      }
    });
  });

  // handle edit of the todo task
  document.querySelectorAll(".edit-btn").forEach((btn) => {
    btn.addEventListener("click", () => {
      const todoId = btn.dataset.todoId;

      fetch(`${BASE_URL}/${todoId}`)
        .then((res) => res.json())
        .then((todo) => openEditModal(todo));
    });
  });

  // handle complete of the todo task
  document.querySelectorAll(".complete-btn").forEach((btn) => {
    btn.addEventListener("click", async () => {
      const todoId = btn.dataset.todoId;
      if (confirm("Mark this todo as completed?")) {
        fetch(`${BASE_URL}/${todoId}`, {
          method: "PATCH",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify([{ op: "replace", path: "/status", value: 2 }]), // 2 = Completed
        }).then(() => getAllTodos());
      }
    });
  });
}

// this open a form with the specified todo data for editing
function openEditModal(todo) {
  const editModal = new bootstrap.Modal("#editModal");
  const form = document.getElementById("editForm");

  const statusValues = {
    Pending: 0,
    InProgress: 1,
    Completed: 2,
  };

  const priorityValues = {
    Low: 0,
    Medium: 1,
    High: 2,
  };

  document.getElementById("editId").value = todo.id;
  document.getElementById("editTitle").value = todo.title;
  document.getElementById("editDescription").value = todo.description || "";
  document.getElementById("editStatus").value = statusValues[todo.status];
  document.getElementById("editPriority").value = priorityValues[todo.priority];
  document.getElementById("editDueDate").value =
    todo.dueDate?.split("T")[0] || "";

  editModal.show();

  form.onsubmit = (e) => {
    e.preventDefault();

    if (!form.checkValidity()) {
      form.classList.add("was-validated");
      return;
    }

    const updatedTodo = {
      id: todo.id,
      title: form.elements.title.value,
      description: form.elements.description.value,
      status: parseInt(form.elements.status.value),
      priority: parseInt(form.elements.priority.value),
      dueDate: form.elements.dueDate.value || null,
    };

    fetch(`${BASE_URL}/${todo.id}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(updatedTodo),
    }).then(() => {
      editModal.hide();
      getAllTodos();
    });
  };
}

function formatDate(dateString) {
  const date = new Date(dateString);
  return date.toLocaleDateString("en-US", {
    year: "numeric",
    month: "short",
    day: "numeric",
  });
}

const createModal = new bootstrap.Modal("#createModal");
document
  .getElementById("openForm")
  .addEventListener("click", () => createModal.show());

// handle form submission when creating a new todo item using bootstrap UI
document.getElementById("createForm").addEventListener("submit", (e) => {
  e.preventDefault();
  const form = e.target;

  if (!form.checkValidity()) {
    form.classList.add("was-validated");
    return;
  }

  const dueDateInput = form.elements.dueDate;
  if (dueDateInput.value) {
    const selectedDate = new Date(dueDateInput.value);
    const today = new Date();
    today.setHours(0, 0, 0, 0);

    console.log(dueDateInput.value);

    if (selectedDate < today) {
      dueDateInput.setCustomValidity("Invalid date");
      dueDateInput.nextElementSibling.textContent =
        "Date must be in the future";
      form.classList.add("was-validated");
      return;
    }
  }

  const newTodo = {
    title: form.elements.title.value,
    description: form.elements.description.value,
    status: parseInt(form.elements.status.value),
    priority: parseInt(form.elements.priority.value),
    dueDate: dueDateInput.value || null,
  };

  form.reset();
  form.classList.remove("was-validated");
  createModal.hide();

  fetch(BASE_URL, {
    method: "POST",
    headers: {
      "Content-Type": "application/json", // Specify JSON content
    },
    body: JSON.stringify(newTodo),
  }).then(() => getAllTodos());
});

// resets and cleans up the form when a Bootstrap modal is closed.
document
  .getElementById("createModal")
  .addEventListener("hidden.bs.modal", () => {
    const form = document.getElementById("createForm");
    form.reset();
    form.classList.remove("was-validated");
    form
      .querySelectorAll(".is-invalid")
      .forEach((el) => el.classList.remove("is-invalid"));
  });

document.addEventListener("DOMContentLoaded", () => {
  getAllTodos();
  addFilterListeners();
});
