/**
 * @project		processEngine_0.1
 * @package		processEngine.core
 * @filename	Token.java
 * @author		Yan Biying
 * @date		2012-03-24
 * @time		����03:13:35
 */
package processEngine.core;

/**
 * @author Yan Biying
 *工作流网模型Token接口。
 */
public interface Token {
	/**
	 * make a copy 
	 * @return clone of this token
	 */
	public Token clone();
	
	/**
	 * destroy his token and recycle resources 
	 */
	public void destroy();
}
