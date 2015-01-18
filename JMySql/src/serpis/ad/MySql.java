package serpis.ad;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;

public class MySql {

	public static void main(String[] args) throws ClassNotFoundException, SQLException {
		//Class.forName("com.mysql.jdbc.Driver"); necesario en tipo 3 o anterior
		System.out.println("Hola MySql desde eclipse");
		Connection connection = DriverManager.getConnection(
			"jdbc:mysql://localhost/dbprueba?user=root&password=sistemas"
		);
		Statement statement = connection.createStatement();
		ResultSet resultSet = statement.executeQuery("select * from categoria");

		while (resultSet.next()) 
			System.out.printf("id=%4s nombre=%s\n", 
				resultSet.getObject("id"), resultSet.getObject("nombre")); 
		
		
		resultSet.close();
		statement.close();
		connection.close();
	}

}
