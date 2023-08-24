var createdTasks = [];
const ul = document.getElementById("taskList");
var input = document.getElementById("inputArea");

input.addEventListener("keypress", function(event){
    if(event.key === "Enter") {
        document.getElementById('saveButton').click();
    }   
});
function saveTask() {

    var text = input.value.trim();
    var isOnListvar = isOnList(text);
    if (isOnListvar == false && text != "") {
        const li = document.createElement("li");
        createdTasks.push(text);
        li.innerHTML = `
            <span>${text}</span>
            <button class="edit">Editar</button>
            <button class="delete">Excluir</button>
        `;
        ul.appendChild(li);

        const editButton = li.querySelector(".edit");
        editButton.addEventListener("click", () => editTask(li));

        const deleteButton = li.querySelector(".delete");
        deleteButton.addEventListener("click", () => deleteTask(li));
        document.getElementById("instructions").textContent = "Digite o nome da tarefa a ser adicionada:";
        input.value = "";

        if (text == "Shrek" || text == "shrek") {
            var img = document.getElementById("shrek");
            img.style.visibility = "visible";
        }
    }

    if (isOnListvar == true) {
        document.getElementById("instructions").textContent = "A tarefa \"" + text + "\" ja esta na lista";
        input.value = "";
    }
}

function isOnList(input) {
    for (i = 0; i < createdTasks.length; i++) {
        if (input == createdTasks[i]) {
            return true;
        }
    }
    return false;
}

function editTask(li) {
    const taskText = li.querySelector("span").textContent;
    const updatedTaskText = prompt("Novo nome da tarefa:", taskText);
    if (updatedTaskText !== null && isOnList(updatedTaskText) == false) {
        removeFromArray(li);
        li.querySelector("span").textContent = updatedTaskText;
        createdTasks.push(updatedTaskText);
    }
}

function deleteTask(li) {
    removeFromArray(li);
    li.remove();
}

function removeFromArray(liToRemove) {
    liText = liToRemove.querySelector("span").textContent;
    for (i = 0; i < createdTasks.length; i++) {
        if (createdTasks[i] == liText) {
            delete createdTasks[i];
        }
    }
}

//Usar SPLICE
