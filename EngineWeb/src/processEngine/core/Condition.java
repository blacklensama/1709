/**
 * @project		processEngine_0.1
 * @package		processEngine.core
 * @filename	Condition.java
 * @author		Yan Biying
 * @date		2012-03-24
 * @time		03:23:28
 */
package processEngine.core;

/**
 * @author Yan Biying
 *工作流网模型的条件接口。
 */
public interface Condition {
	
	public boolean pass(Token token);

}
