package processEngine.parse;

import org.dom4j.Element;

import processEngine.core.Place;
import processEngine.entry.Process;

/*
 * 任务模型解析接口
 */
public interface IParser {
	public Place parse(Process process,Place sp,Element node,Place op,boolean asEquivalent);
}
