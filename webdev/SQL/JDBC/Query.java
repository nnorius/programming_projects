import java.sql.*;

public class Query {

public static void main(String args[]) throws SQLException {
		
		Connection conn = getConnection();
		

		//Query tables 
		
		String[] queryStatements = getQueryStatements();
		
		try (var statement = conn.createStatement()){
					
			ResultSet rs = statement.executeQuery(queryStatements[0]);
			while (rs.next()) {
		        int snum = rs.getInt("snum");
		        int ssn = rs.getInt("ssn");
		        System.out.println(snum + ", " + ssn);
			}
		    rs = statement.executeQuery(queryStatements[1]);
		    while (rs.next()) {   
		        String name = rs.getString("name");
		        String level = rs.getString("level");
		        System.out.println(name + ", " + level);
		      }
		    
		    rs = statement.executeQuery(queryStatements[2]);
		    while (rs.next()) {   
		        String name = rs.getString("name");
		        System.out.println(name);
		      }
		    
		    rs = statement.executeQuery(queryStatements[3]);
		    while (rs.next()) {   
		        String name = rs.getString("name");
		        String level = rs.getString("level");
		        System.out.println(name + ", " + level);
		      }
				
		    rs = statement.executeQuery(queryStatements[4]);
		    while (rs.next()) {   
		        String name = rs.getString("name");
		        System.out.println(name);
		      }
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
		String q1 = "select snum, ssn from students where name = 'Becky'";
		String q2 ="select m.name, m.level from students s inner join major m on s.snum = m.snum where s.ssn = '123097834'";
		String q3 = "select c.name from courses c inner join departments d on c.department_code = d.code where d.name = 'Computer Science'";
		String q4 = "select d.name, d.level from degrees d inner join departments dd on d.department_code = dd.code where dd.name = 'Computer Science'";
		String q5 = "select distinct s.name from students s inner join minor m on s.snum = m.snum";
		
		String statement[] = new String[] {q1, q2, q3, q4, q5};
		return statement;
	}
}
