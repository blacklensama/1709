/**
 * 
 */
package com.supermap;

import java.io.ByteArrayOutputStream;

import android.content.Context;
import android.database.Cursor;
import android.database.SQLException;
import android.database.sqlite.SQLiteDatabase;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.util.Base64;

/**
 * @author liuhong
 * 
 */
public class ImgDatabase {
	private DBOpenHelp dbOpenHelper;
	// private Context context;
	private SQLiteDatabase db;

	/**
	 * 
	 */
	public ImgDatabase(Context context) {
		// TODO Auto-generated constructor stub
		// File SDCardRoot = Environment.getExternalStorageDirectory();
		// String strDir = SDCardRoot.getAbsolutePath();
		// strDir = (new
		// StringBuilder(String.valueOf(strDir))).append("/SuperMap/").toString();
		// File dirFile = new File(strDir);
		// if(!dirFile.exists())
		// dirFile.mkdirs();
		// dirFile = null;

		// strDir = "";
		dbOpenHelper = new DBOpenHelp(context, RequestControl.dir
				+ RequestControl.dbName);
		db = dbOpenHelper.getWritableDatabase();
	}

	public void saveByte(String x, String y, String z, byte[] data) {
		// ���Ҫ�����ݽ��и��ģ��͵��ô˷����õ����ڲ������ݿ��ʵ��,�÷���һ����д��ʽ�����ݿ�
		// SQLiteDatabase db = dbOpenHelper.getWritableDatabase();
		// ���ֻ�����ݿ���ж�ȡ������ʹ�ô˷���,���˷�����������ֻ����ʽ�����ݿ�ġ���Ϊ�÷����ڲ�������getWritableDatabase����
		// SQLiteDatabase db2 = dbOpenHelper.getReadableDatabase();
		// ��ռλ���ɷ�ֹ�û������������
		String key = x + "_" + y + "_" + z;
		String sql = "insert into tiles (tile_column,tile_row,zoom_level,tile_data) values (?,?,?,?);";
		Object[] args = new Object[] { x, y, z, data };
		try {
			db.execSQL(sql, args);
			// db.compileStatement(sql)(sql,args);
			RequestControl.keyMap.put(key, 1);
		} catch (SQLException ex) {
			System.out.println(ex.getMessage());
		}
		// ��������ݿ�ֻ����Ӧ�÷��ʣ�android������ر����ݿ⣬Ϊ������
		// db.close();
	}

	public byte[] queryByte(String x, String y, String z) {
		byte[] imgdata = null;
		// SQLiteDatabase db = dbOpenHelper.getReadableDatabase();
		try {
			int y1 = (1 << Integer.parseInt(z)) - Integer.parseInt(y) - 1;
			y = y1 + "";
			String sql = "select tile_data from tiles where tile_column=? and tile_row=? and zoom_level=?";
			Cursor cursor = db.rawQuery(sql, new String[] { x, y, z });
			if (cursor.moveToFirst()) {
				imgdata = cursor.getBlob(0);
			}
			cursor.close();
		} catch (Exception e) {
			System.out.println(e.getMessage());
		}

		// db.close();
		return imgdata;
	}

	public void queryImg(String x, String y, String z) {
		byte[] imgdata = this.queryByte(x, y, z);

		BitmapFactory.Options opts = new BitmapFactory.Options();
		// opts.inJustDecodeBounds = true;
		Bitmap map = BitmapFactory.decodeByteArray(imgdata, 0, imgdata.length);
	}

	public String queryImgBase64(String x, String y, String z) {
		String dataStr = "null";
		byte[] imgdata = this.queryByte(x, y, z);

		// BASE64Encoder encoder = new BASE64Encoder();
		// dataStr = encoder.encode(imgdata);
		// dataStr = Convert.ToBase64String(imgdata);
		if (imgdata != null) {
			dataStr = Base64.encodeToString(imgdata, Base64.DEFAULT);
			dataStr = dataStr.replaceAll("\n", "");
		}

		return dataStr;
	}

	public boolean isExist(String x, String y, String z) {
		boolean isExist = false;

		int y1 = (1 << Integer.parseInt(z)) - Integer.parseInt(y) - 1;
		y = y1 + "";

		System.out.println("isExist:x=" + x + ",y=" + y + ",z=" + z);
		// SQLiteDatabase db = dbOpenHelper.getReadableDatabase();
		String sql = "select count(*) from tiles where tile_column=? and tile_row=? and zoom_level=?";
		Cursor cursor = db.rawQuery(sql, new String[] { x, y, z });
		cursor.moveToFirst();
		int count = cursor.getInt(0);
		if (count > 0) {
			isExist = true;
		}
		// db.close();
		return isExist;
	}

	// public boolean isExist(String key){
	// boolean isExist = false;
	//
	// if(RequestControl.keyMap.containsKey(key)){
	// isExist = true;
	// }
	// return isExist;
	// }

	public String saveMap(Bitmap map, String x, String y, String z) {
		ByteArrayOutputStream baos = new ByteArrayOutputStream();
		map.compress(Bitmap.CompressFormat.JPEG, 80, baos);
		byte[] imgdata = baos.toByteArray();
		this.saveByte(x, y, z, imgdata);

		String dataStr = Base64.encodeToString(imgdata, Base64.DEFAULT);
		dataStr = dataStr.replaceAll("\n", "");
		return dataStr;
	}

	public void close() {
		this.dbOpenHelper.close();
	}

	/**
	 * @param args
	 */
	public static void main(String[] args) {
		// TODO Auto-generated method stub

	}

}
