/**
 * @project		processEngine_0.1
 * @package		processEngine.core
 * @filename	Place.java
 * @author		Yan Biying
 * @date		2012-03-24
 * @time		����03:17:21
 */
package processEngine.core;

/**
 * @author Yan Biying
 *工作流网模型的库所接口。
 */
public interface Place {
	/**
	 * When a Token arrives a Place 
	 * @param token
	 */
	public void arrive(Token token);
	
	/**
	 * try to fetech a token from the Place
	 * @return null indicates no token available at present
	 */
	public Token fetch();
}
