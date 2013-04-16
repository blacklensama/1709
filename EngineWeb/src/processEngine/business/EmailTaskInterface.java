/**
 * @project		processEngine_0.1
 * @package		processEngine.core
 * @filename	Transition.java
 * @author		Yan Biying
 * @date		2012-03-24
 * @time		����03:14:40
 */
package processEngine.business;

import java.io.Serializable;

/**
 * @author Yan Biying
 *工作流网模型Transition接口。
 */
public abstract class EmailTaskInterface implements Serializable{

	/**
	 * Barrier - placed in front of a Transition
	 * @param token input Transition
	 * @param src source of this token
	 * @return ready tokens if there is any
	 */
	public void startTask(){};
}
