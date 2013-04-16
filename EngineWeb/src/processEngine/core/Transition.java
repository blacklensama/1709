/**
 * @project		processEngine_0.1
 * @package		processEngine.core
 * @filename	Transition.java
 * @author		Yan Biying
 * @date		2012-03-24
 * @time		����03:14:40
 */
package processEngine.core;

/**
 * @author Yan Biying
 *工作流网模型Transition接口。
 */
public interface Transition {

	/**
	 * Barrier - placed in front of a Transition
	 * @param token input Transition
	 * @param src source of this token
	 * @return ready tokens if there is any
	 */
	public Token[] barrier(Token token, Place src);
	
	/**
	 * The logic of Transition
	 * @param tokens input token list
	 * @return output token list
	 */
	public Token[] process(Token[] tokens);
}
