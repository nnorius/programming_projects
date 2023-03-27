
//load csv students
LOAD CSV with headers FROM 'https://docs.google.com/spreadsheets/d/e/2PACX-1vROrskhgTXDn0YC0bMnn9pSZG0fguBT8qSemdN8iJyNqhhn9bQ6z5X-3Lw1I3lFBmNyfDVohA4ZfOHi/pub?gid=946458172&single=true&output=csv'
AS line
CREATE (:students { snum:toInteger(line.snum),ssn:toInteger(line.ssn), name:line.name,gender:line.gender, dob:line.dob, c_addr:line.c_addr, c_phone:line.c_phone, p_addr:line.p_addr, p_phone:line.p_phone});
create constraint constraint1 for (d:students) require d.snum is unique;
create constraint constraint3 for (d:students) require d.ssn is unique;
//set dob to datetime
MATCH (s:students)
SET s.dob = datetime(s.dob)
return s.dob;

//load csv departments
LOAD CSV with headers FROM 'https://docs.google.com/spreadsheets/d/e/2PACX-1vSZjB-njgyoNAS7hhgvyqPMKs7gSEAzRzj55XrDsS4Ly_Q5pS2XWIWe-Qnx_UsszAxs7GZDT8z9N6l6/pub?gid=1270533085&single=true&output=csv
' AS line
CREATE (:department { dcode:toInteger(line.dcode),dname:line.name,phone:toInteger(line.phone),college:line.college});

create constraint constraint2 for (d:department) require d.dcode is unique;


//load degree
LOAD CSV with headers FROM 'https://docs.google.com/spreadsheets/d/e/2PACX-1vQmzlXmZkQbRaV4gcSaMRINRlEypaOZjMNBNutAHj2Eij4CgpfYni54oj5pFtNAb1k5vCu6pQlsuSBN/pub?gid=1817942933&single=true&output=csv
' AS line
CREATE (dgr:degree {dname:line.name,level:line.level})
WITH dgr, line
MATCH (dep: department {dcode:toInteger(line.dcode)})
CREATE (dgr)<-[:administer]-(dep);


//load courses
LOAD CSV with headers FROM 'https://docs.google.com/spreadsheets/d/e/2PACX-1vQl4ewanefLXz_qsNa1FEYimkQoPWLighjsYtUicQ2ZAfje3BJNvIq2LHLMsTtNhSRq3bdDudFwSA47/pub?gid=1395040011&single=true&output=csv'
AS line
CREATE (crs:course {number:toInteger(line.number),name:line.name,description:line.description,credithours:toInteger(line.credithours),level:line.level,dcode:toInteger(line.dcode)})
WITH crs, line
MATCH (dep: department {dcode:toInteger(line.dcode)})
CREATE (crs)<-[:offers]-(dep);

create constraint constraint4 for (c:courses) require c.number is unique;


//register
LOAD CSV with headers FROM 'https://docs.google.com/spreadsheets/d/e/2PACX-1vT7DgJc6JrTXz0JI5QUy4Vf8EbUipytjzLBLTRYe9qCQai62-4wxQFAnW5VUzHI0YZcKzt3_aGZS_GD/pub?gid=147295713&single=true&output=csv'
AS line
WITH line
MATCH (s:students {snum:toInteger(line.snum)})
MATCH (c:course {number:toInteger(line.course_number)})
CREATE (c)<-[:register{regtime:line.regtime, grade:toInteger(line.grade)}]-(s);


//major
LOAD CSV with headers FROM 'https://docs.google.com/spreadsheets/d/e/2PACX-1vShYSEUl5j9HpV0GngP4AIk_3wlIfQXK-OF3zCxiL6TkDZ69ultb734LN5LPBq9oKAfcl-T1iqQkS1q/pub?gid=995703209&single=true&output=csv'
AS line
WITH line S
MATCH (s: students {snum:toInteger(line.snum)})
MATCH (d: degree {name:line.name})
MATCH (d: degree {level:line.level})
CREATE (d)<-[:major]-(s);

//minor
LOAD CSV with headers FROM 'https://docs.google.com/spreadsheets/d/e/2PACX-1vQEhGIicNxhMTy6z0bpzPZIlSneFOorg0Sp6ApOFlHf0uz3aNuZAHB1KoXNssjqrGPksWXjEdBJeZ1j/pub?gid=1971279781&single=true&output=csv'
AS line
WITH line
MATCH (s: students {snum:toInteger(line.snum)})
MATCH (d: degree {name:line.name})
MATCH (d: degree {level:line.level})
CREATE (d)<-[:minor]-(s);
