import React, { useEffect, useState } from "react";
import api from "./api/axios";

function Tasks() {
    const [tasks, setTasks] = useState([]);

    useEffect(() => {
        api.get("/tasks")
            .then(response => {
                setTasks(response.data);
            })
            .catch(error => {
                console.error("Error fetching tasks:", error);
            });
    }, []);

    return (
        <div>
            <h1>Task List</h1>
            <ul>
                {tasks.map(task => (
                    <li key={task.id}>{task.title}</li>
                ))}
            </ul>
        </div>
    );
}

export default Tasks;
