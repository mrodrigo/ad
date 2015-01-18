package serpis.ad;

public class Vector {

	public static void main(String [] args){
		int[] array = new int[] {13, 20, 4, 7, 9, 12};
		min(array);
	}

	public static int min(int[] array){
		int minimo = array[0];
		for (int i=1; i<array.length; i++){
			if(array[i]<minimo){
				minimo = array[i];
			}
		}
		System.out.println(minimo);
		return minimo;

	}
}	
