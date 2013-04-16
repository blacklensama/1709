package processEngine.test;
import static org.junit.Assert.assertEquals;
import static org.junit.Assert.fail;

import org.junit.After;
import org.junit.AfterClass;
import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.Ignore;
import org.junit.Test;
import org.junit.internal.runners.TestClassRunner;
import org.junit.runner.RunWith;
//默认如此  测试运行器
@RunWith(TestClassRunner.class)
public class JunitTest {
	@Test
	public void hello1() {
		System.out.println("hello-1");
		fail("Hello, JUnit!");
	}
	@Test(timeout=1000)
	public void hello2() {
		System.out.println("hello-2");
		new java.util.Random().nextBoolean();
		while(true);
	}
	@Ignore("shit")
	@Test
	public void hello3() {
		System.out.println("hello-3");
		assertEquals(60,20+40);
	}
	@Before
	public void sayHello(){
		System.out.println("Hello");
	}
	@After
	public void sayBye(){
		System.out.println("Bye");
	}
	@BeforeClass
	public static void sayHelloClass(){
		System.out.println("HelloClass");
	}
	@AfterClass
	public static void sayByeClass(){
		System.out.println("ByeClass");
	}
}