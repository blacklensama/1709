package processEngine.nioserver.event;

import processEngine.nioserver.Request;
import processEngine.nioserver.Response;

/**
 * <p>Title: �������¼�������</p>
 * @author YBY
 */

public interface ServerListener {

   /**
    * �������˴���������ʱ�������¼�
    * @param error ������Ϣ
    */
   public void onError(String error);

   /**
    * ���пͻ��˷�������ʱ�������¼�
    */
   public void onAccept() throws Exception;

   /**
    * ������˽��ܿͻ�������󴥷����¼�
    * @param request �ͻ�������
    */
   public void onAccepted(Request request) throws Exception;

   /**
    * ���ͻ��˷�����ݣ����ѱ������������߳���ȷ��ȡʱ���������¼�
    * @param request �ͻ�������
    */
   public void onRead(Request request) throws Exception;

   /**
    * ��������ͻ��˷������󴥷����¼�
    * @param request �ͻ�������
    */
   public void onWrite(Request request, Response response) throws Exception;

   /**
    * ���ͻ�����������������Ӻ󴥷����¼�
    * @param request �ͻ�������
    */
   public void onClosed(Request request) throws Exception;
}
