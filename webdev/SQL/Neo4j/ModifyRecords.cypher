//1)	Change the name of the student with ssn = 746897816 to Scott
MATCH (n: students)
WHERE n.ssn = 746897816
SET n.name = "Scott";

//2)	Change the major of the student with ssn = 746897816 to Computer Science, Master.
MATCH (n:students)-[:major]->(d:degree)
WHERE n.ssn = 746897816
SET d.name = "Computer Science" AND d.level = "MS";

//3)	Delete all registration records that were in “Spring2021”
MATCH (n:students)-[r:register]->(c:course)
WHERE r.regtime = "Spring2021"
DETACH DELETE rs;
