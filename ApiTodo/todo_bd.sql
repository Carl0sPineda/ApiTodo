CREATE DATABASE TODO_APP
GO
USE TODO_APP
GO

CREATE TABLE tasks (
  id int identity(1,1) primary key NOT NULL,
  title varchar(40) NOT NULL,
  autor varchar(30) NOT NULL,
  status_task varchar(25) NOT NULL,
  description varchar(150) NOT NULL,
  start_date date NOT NULL,
  end_date date NOT NULL,
);
GO

CREATE PROCEDURE sp_GetTaskById 
  @id int
AS
BEGIN
  SELECT * FROM tasks
  WHERE id = @id
END
GO

CREATE PROCEDURE sp_GetTaskByStatus
  @status_task VARCHAR(25)
AS
BEGIN
  SELECT * 
  FROM tasks
  WHERE status_task = @status_task
  ORDER BY id DESC;
END
GO

CREATE PROCEDURE sp_AddTask 
  @title varchar(40),
  @autor varchar(30),
  @status_task varchar(25),
  @description varchar(150),
  @start_date date,
  @end_date date
AS
BEGIN
  INSERT INTO tasks
    VALUES (@title, @autor, @status_task,@description,@start_date,@end_date)
END
GO

CREATE PROCEDURE sp_UpdateTask
  @id int,
  @title varchar(40),
  @autor varchar(30),
  @status_task varchar(25),
  @description varchar(150),
  @start_date date,
  @end_date date
AS
BEGIN
  UPDATE tasks
  SET title = @title,
    autor = @autor,
    status_task = @status_task,
    description = @description,
    start_date = @start_date,
    end_date = @end_date
  WHERE id = @id;
END
GO

CREATE PROCEDURE sp_UpdateTaskStatus
  @id int,
  @status_task varchar(25)
AS
BEGIN
  UPDATE tasks
  SET status_task = @status_task
  WHERE id = @id;
END
GO

CREATE PROCEDURE sp_DeleteTask
  @id int
AS
BEGIN
  DELETE tasks
  WHERE id = @id
END
GO