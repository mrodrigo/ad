package serpis.ad;

import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.List;

import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import javax.persistence.Persistence;


public class HibernateCategoria {
	
	//Los métodos setup y teardown se ejecutan justo antes y después de los métodos de test.
	//Debemos introducir la creación del entityManagerFactory en el setup y el close() en teardown.
	
	private static EntityManagerFactory entityManagerFactory;

	public static void main(String[] args) {
		
		entityManagerFactory = Persistence.createEntityManagerFactory("serpis.ad.hibernate.mysql");
		
		showCategorias();
		System.out.println("Añadiendo categoria...");
		persistNuevasCategorias();
		
		entityManagerFactory.close();

	}
	public static void persistNuevasCategorias(){
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		
		Categoria categoria = new Categoria();
		categoria.setNombre("Hibernate -> " + new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(new Date()));
		
		entityManager.persist(categoria);
		
		entityManager.getTransaction().commit();
		entityManager.close();
	}
	
	public static void showCategorias(){
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		
		List<Categoria> categorias = entityManager.createQuery("FROM Categoria", Categoria.class).getResultList();
		for (Categoria categoria : categorias)
			System.out.printf("id=%d nombre=%s\n", categoria.getId(), categoria.getNombre());
		
		entityManager.getTransaction().commit();
		entityManager.close();
	}

}
