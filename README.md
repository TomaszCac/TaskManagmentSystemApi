# TaskManagmentSystemApi
This project is supposed to be for CV. It is supposed to show the flow of my Git and how i code.\
TaskManagmentSystemApi is a Task Managment System designed to create tasks, assigning them to other users, comment on them etc.\
There's 3 models : User, Task, Comment.
## Endpoints:
### Users
POST /api/users: register a new user (email verification)\
POST /api/users/login: user login (returns JWT token)\
GET /api/users/{id}: get user details\
PUT /api/users/{id}: update user details\
DELETE /api/users/{id}: delete user

### Tasks
POST /api/tasks: create a new task (authentication required)\
GET /api/tasks: get a list of tasks (with filtering options based on status, priority, and assignee)\
GET /api/tasks/{id}: get task details\
PUT /api/tasks/{id}: update a task (status change, reassign user, update priority)\
DELETE /api/tasks/{id}: delete a task

### Comments
POST /api/tasks/{taskId}/comments: add a comment to a task\
GET /api/tasks/{taskId}/comments: get comments for a task\
DELETE /api/comments/{id}: delete a comment

    
