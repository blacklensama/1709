package processEngine.test;

public class Test {

	/**
	 * @param args
	 */
	public static void main(String[] args) {
		// TODO Auto-generated method stub
		System.out.println(System.getProperty("java.library.path"));
		System.out.println(System.getProperty("user.name"));
		System.setProperty("chenjian", "chenjian");
		System.out.println(System.getProperty("chenjian"));
		System.getProperties().list(System.out);
		
	}

}
