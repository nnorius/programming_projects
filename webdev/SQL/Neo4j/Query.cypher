//1)	The student number and ssn of the student whose name is "Becky"
MATCH (ee:students)
WHERE ee.name = "Becky"
RETURN ee.snum, ee.ssn;

//2)	The major name and major level of the student whose ssn is 123097834
MATCH (ee:students)-[:major]-(d:degree)
WHERE ee.ssn = 123097834
RETURN d.dname, d.level

//3)All degree names and levels offered by the department Computer Science
MATCH (d:department)-[:administer]->(deg:degree)
WHERE d.dname = "Computer Science"
RETURN deg.dname, deg.level

//4)	The names of all students who have a minor
MATCH (ee:students)-[:minor]-()
RETURN ee.name

//5)	The count of students who have a minor
MATCH (n:students)-[:minor]->()
RETURN count(*) AS nbr

//6)	The names and snums of all students enrolled in course “Algorithm”
MATCH (n:students)-[:register]->(c:course)
WHERE c.name = "Algorithm"
RETURN n.name, n.snum

//7)	The names of all students who enrolled in course 363 and their corresponding grade
MATCH (n:students)-[r:register]->(c:course)
WHERE c.number = 363
RETURN n.name, r.grade

//8)	The name and snum of the oldest student
MATCH (e:students)
WITH duration.between(date(e.dob), date()) as d
RETURN max(d)

//9)	The name and snum of the youngest student
MATCH (e:students)
return e.name, e.snum
ORDER BY e.dob DESC
LIMIT  1

//10)	The name, snum and SSN of the students whose name contains letter “n” or “N”
MATCH (n:students)
WHERE n.name CONTAINS 'N'
    OR n.name CONTAINS 'n'
RETURN n.name, n.snum, n.ssn

//11)	The name, snum and SSN of the students whose name does not contain letter “n” or “N”
MATCH (n:students)
WHERE NOT(n.name CONTAINS 'N'
    OR n.name CONTAINS 'n')
RETURN n.name, n.snum, n.ssn

//12)	The course number, name and the number of students registered for each course
MATCH (c:course)
OPTIONAL MATCH (c)<-[r:register]-(s:students)
RETURN c.name, c.number, count(s.snum)

//13)	The name of the students enrolled in Fall2020 semester.
MATCH (n:students)-[r:register]->()
WHERE r.regtime = "Fall2020"
RETURN n.name

//14)	The course numbers and names of all courses offered by Department of Computer Science
MATCH (d:department)-[:offers]->(c:course)
WHERE d.dname = "Computer Science"
RETURN c.name, c.number

//15)	The course numbers and names of all courses offered by either Department of Computer Science or Department of Landscape Architect.
MATCH (d:department)-[:offers]->(c:course)
WHERE d.dname = "Computer Science" OR d.dname = "Landscape Architect"
RETURN c.name, c.number
