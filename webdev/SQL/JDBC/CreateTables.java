import java.sql.*;

public class CreateTables {

	public static void main(String args[]) throws SQLException {
		
		Connection conn = getConnection();
		
		//Create tables
		String[] createStatements = getCreateStatements();
		
		try (var statement = conn.createStatement()){
			conn.setAutoCommit(false);
			for (String stmt: createStatements) {
				statement.addBatch(stmt);
			}
			statement.executeBatch();
			conn.commit();
			conn.setAutoCommit(true);
		    System.out.println("Created tables");
		}
	}
	
	public static Connection getConnection() throws SQLException{
		
		Connection conn = null;	
		String userName = "root";
		String password = "main";
		String dbServer = "jdbc:mysql://localhost:3306/university";
		conn = DriverManager.getConnection(dbServer,userName,password);
		System.out.println("Connected to the database");
		return conn;
		
	}
	
	public static String[] getCreateStatements() {
		 /* String students = "CREATE TABLE STUDENTS " +
                "(snum INTEGER DEFAULT NULL UNIQUE, " +
                " ssn INTEGER NOT NULL, " +
                " name VARCHAR(10) DEFAULT NULL, " +
                " gender VARCHAR(1) DEFAULT NULL, " +
                " dob DATETIME DEFAULT NULL," +
                " c_addr VARCHAR(20) DEFAULT NULL, " +
                " c_phone VARCHAR(10) DEFAULT NULL, " +
                " p_addr VARCHAR(20) DEFAULT NULL, " +
                " p_phone VARCHAR(10) DEFAULT NULL, " +	                   
                " PRIMARY KEY ( ssn ))" ;
              
		String departments = "CREATE TABLE DEPARTMENTS " +
                "(code INTEGER NOT NULL, " +	                   
                " name VARCHAR(50) DEFAULT NULL UNIQUE, " +
                " phone VARCHAR(10) DEFAULT NULL, " +	                 
                " college VARCHAR(20) DEFAULT NULL, " +	                   	                   
                " PRIMARY KEY ( code ))" ;*/
		
		
		
		
		String students = "CREATE TABLE STUDENTS" +
				  "(snum int(11) NOT NULL, " +
				  " ssn int(11) NOT NULL, " +
				  " name varchar(10) DEFAULT NULL, " +
				  " gender varchar(1) DEFAULT NULL, " +
				  " dob datetime DEFAULT NULL, " +
				  " c_addr varchar(20) DEFAULT NULL, " +
				  " c_phone varchar(10) DEFAULT NULL, " +
				  "p_addr varchar(20) DEFAULT NULL, " +
				  " p_phone varchar(10) DEFAULT NULL, " +
				  " PRIMARY KEY ( snum ))";
				
		String departments =  "CREATE TABLE DEPARTMENTS " +
				  "(code int(11) NOT NULL, " +
				  " name varchar(50) NOT NULL, " +
				  "phone varchar(10) DEFAULT NULL, " +
				  "college varchar(20) DEFAULT NULL, " +
				  "PRIMARY KEY ( code ))" ;
			
		String degrees = "CREATE TABLE DEGREES " +
				  "(name varchar(50) NOT NULL, " +
				  " level varchar(5) NOT NULL, " +
				  " department_code int(11) DEFAULT NULL, " +
				  " PRIMARY KEY (name,level), " +
				  " KEY degrees_2_departments_idx (department_code), " +
				  " KEY degrees_4_major1_idk (name), " +
				  " KEY degrees_4_major2_idk (level), " +
				  " CONSTRAINT degrees_2_departments FOREIGN KEY (department_code) REFERENCES departments (code) ON DELETE CASCADE ON UPDATE CASCADE)" ;
				
		String major = "CREATE TABLE MAJOR" + 
				  "(snum int(11) NOT NULL, " +
				  " name varchar(50) NOT NULL, " +
				  " level varchar(5) NOT NULL, " +
				  " PRIMARY KEY (snum,name,level), " +
				  " KEY major_2_degrees1_idx (name), " +
				  " KEY major_2_degrees2_idx (level), " +
				  " CONSTRAINT major_2_students FOREIGN KEY (snum) REFERENCES students (snum) ON DELETE CASCADE ON UPDATE CASCADE, " +
				  " CONSTRAINT major_2_degrees1 FOREIGN KEY (name) REFERENCES degrees (name) ON DELETE CASCADE ON UPDATE CASCADE, " +
				  " CONSTRAINT major_2_degrees2 FOREIGN KEY (level) REFERENCES degrees (level) ON DELETE CASCADE ON UPDATE CASCADE )";
			
		String minor = "CREATE TABLE MINOR" +
				  "(snum int(11) NOT NULL, " +
				  " name varchar(50) NOT NULL, " +
				  " level varchar(5) NOT NULL, " +
				  " PRIMARY KEY (snum, name, level), " +
				  " KEY minor_2_degrees1_idx (name), " +
				  " KEY minor_2_degrees2_idx (level), " +
				  " CONSTRAINT minor_2_students FOREIGN KEY (snum) REFERENCES students (snum) ON DELETE NO ACTION ON UPDATE NO ACTION, " +
				  " CONSTRAINT minor_2_degrees1 FOREIGN KEY (name) REFERENCES degrees (name) ON DELETE NO ACTION ON UPDATE NO ACTION, " +
				  " CONSTRAINT minor_2_degrees2 FOREIGN KEY (level) REFERENCES degrees (level) ON DELETE NO ACTION ON UPDATE NO ACTION )" ;
		
		String courses = "CREATE TABLE COURSES" +
				  "(number int(11) NOT NULL, " +
				  " name varchar(50) NOT NULL, " +
				  " description varchar(50) DEFAULT NULL, " +
				  " credithours int(11) DEFAULT NULL, " +
				  " level varchar(20) DEFAULT NULL, " +
				  " department_code int(11) DEFAULT NULL, " +
				  " PRIMARY KEY (number), " +
				  " KEY courses_2_departments_idx (department_code), " +
				  " CONSTRAINT courses_2_departments FOREIGN KEY (department_code) REFERENCES departments (code) ON DELETE NO ACTION ON UPDATE NO ACTION )" ;
			
		String register = "CREATE TABLE REGISTER" +
				  "(snum INTEGER(11) NOT NULL, " +
				  " course_number INTEGER(11) NOT NULL, " +
				 " regtime VARCHAR(20) DEFAULT NULL, " +
				  " grade INTEGER(11) DEFAULT NULL, " +
				  " PRIMARY KEY (snum,course_number), " +
				  " KEY register_2_courses_idx (course_number), " +
				  " CONSTRAINT register_2_students FOREIGN KEY (snum) REFERENCES students (snum) ON DELETE NO ACTION ON UPDATE NO ACTION, " +
				  " CONSTRAINT register_2_courses FOREIGN KEY (course_number) REFERENCES courses (number) ON DELETE NO ACTION ON UPDATE NO ACTION )" ;
					
		
		String statement[] = new String[] {students, departments, degrees, major, minor, courses, register};
		return statement;
		
	}
}
