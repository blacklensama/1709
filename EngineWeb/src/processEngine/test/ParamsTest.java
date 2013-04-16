package processEngine.test;
import static org.junit.Assert.assertEquals;

import java.util.Arrays;
import java.util.Collection;

import org.junit.Test;
import org.junit.runner.RunWith;
import org.junit.runners.Parameterized;
import org.junit.runners.Parameterized.Parameters;
@RunWith(Parameterized.class) 
public class ParamsTest{
	private int param;  // 参数 
	private double result; 
	@Parameters 
	public static Collection<Object[]> dataSet(){ 
		return Arrays.asList(new Object[][]{
				{2,3},
				{1,10},
				{0,1},
				{1,0},
				{2,4}
		});
	}
	
	public ParamsTest(double result,int param){
		this.result = result;
		this.param = param;
	}
	@Test
	public void isOdd(){
		assertEquals(result, Math.log(param));
	}
}
