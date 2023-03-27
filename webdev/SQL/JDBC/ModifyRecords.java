import java.sql.*;


public class ModifyRecords {
	
	public static void main(String args[]) throws SQLException {
		
	Connection conn = getConnection();
	

	//Query tables 
	
	String[] queryStatements = getQueryStatements();
	
	try (var statement = conn.createStatement()){
		
		statement.executeUpdate(queryStatements[0]);
		statement.executeUpdate(queryStatements[1]);
		statement.executeUpdate(queryStatements[2]);
		
		//Test
	/*	String test1 = "select name from students where ssn = '746897816'";		
		ResultSet rs = statement.executeQuery(test1);
		while (rs.next()) {
	        String name = rs.getString("name");
	        System.out.println(name);
		}
		String test2 = "select name, level from major where snum = (select snum from students where ssn = '746897816')";
		
		rs = statement.executeQuery(test2);
		while (rs.next()) {
	        String name = rs.getString("name");
	        String level = rs.getString("level");
	        System.out.println(name + ", " + level);
			
		}
		String test3 = "select regtime from register";
		rs = statement.executeQuery(test3);
		while (rs.next()) {
	        String regtime = rs.getString("regtime");
	        System.out.println(regtime);
			
		}*/
		
	
	
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

public static String[] getQueryStatements() {
	String q1 = "update students set name = 'Scott' where ssn = '746897816'";	
	String q2 = "update major set name = 'Computer Science', level = 'MS' where snum = (select snum from students where ssn = '746897816')";
	String q3 = "delete from register where regtime = 'Spring2021'";
			
	String statement[] = new String[] {q1,q2,q3};
	return statement;
}
}
