-- Author: Norfinn Norius

-- use project1;

-- 1)	Change the name of the student with ssn = 746897816 to Scott
UPDATE students
SET name = 'Scott'
WHERE ssn = '746897816';


-- 2)	Change the major of the student with ssn = 746897816 to Computer Science, Master. 
UPDATE major, students
SET major.name = 'Computer Science'
WHERE students.ssn = '746897816' and major.snum = students.snum;


-- 3)	Delete all registration records that were in “Spring2021”
-- SET SQL_SAFE_UPDATES = 0;
DELETE FROM register WHERE register.regtime = 'Spring2021';
