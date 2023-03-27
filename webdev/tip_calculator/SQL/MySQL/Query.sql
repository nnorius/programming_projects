-- Author: Norfinn Norius
-- use project1;

-- 1)	The student number and ssn of the student whose name is "Becky"

SELECT
     snum,
     ssn
FROM
     students
WHERE
     name = 'Becky'
;


-- 2)	The major name and major level of the student whose ssn is 123097834
SELECT
     major.name,
     major.level,
     students.name
FROM
     major JOIN students 
WHERE
     major.snum = students.snum AND students.ssn = '123097834'
; 

-- 3)	The names of all courses offered by the department of Computer Science
SELECT
  name   
FROM
     courses
WHERE
	department_code = 401
;

-- 4)	All degree names and levels offered by the department Computer Science
SELECT
  name, level   
FROM
     degrees
WHERE
	department_code = 401
;

-- 5)	The names of all students who have a minor

SELECT students.name
FROM students
INNER JOIN minor ON students.snum=minor.snum;
;

-- 6)	The count of students who have a minor
SELECT COUNT(*)
FROM minor
;

-- 7)	The names and snums of all students enrolled in course “Algorithm”
SELECT register.snum, students.name
FROM register, students, courses
WHERE register.course_number = courses.number and register.snum = students.snum and courses.name = 'Algorithm'
;



-- 8)	The name and snum of the oldest student
SELECT
     snum,
     name
FROM
     students
WHERE
	dob = (select min(dob) from students) 
;

-- 9)	The name and snum of the youngest student
SELECT
     snum,
     name
FROM
     students
WHERE
	dob = (select MAX(dob) from students) 
;


-- 10)	The name, snum and SSN of the students whose name contains letter “n” or “N”
SELECT
     snum, name, ssn
FROM
     students
WHERE
	name like '%n%' or name like '%N%'
;


-- 11)	The name, snum and SSN of the students whose name does not contain letter “n” or “N”
SELECT
     snum, name, ssn
FROM
     students
WHERE
	name not like '%n%' and not '%N%'
;


-- 12)	The course number, name and the number of students registered for each course
SELECT courses.number, courses.name,
	 (
        SELECT Count(*) 
        FROM register 
        WHERE courses.number = register.course_number
	)
FROM courses

;


-- 13)	The name of the students enrolled in Fall2020 semester.
SELECT 
	students.name
FROM 
	students, register
WHERE 
	register.regtime = 'Fall2020' AND register.snum = students.snum

;


-- 14)	The course numbers and names of all courses offered by Department of Computer Science
SELECT
  number, name  
FROM
	courses
WHERE
	department_code = 401
;


-- 15)	The course numbers and names of all courses offered by either Department of Computer Science or Department of Landscape Architect.
SELECT
  number, name  
FROM
	courses
WHERE
	department_code = 401 OR department_code = 404
;