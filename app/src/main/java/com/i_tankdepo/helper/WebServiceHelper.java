package com.i_tankdepo.helper;

import android.os.AsyncTask;


import com.i_tankdepo.interfaces.AsyncResponseListener;
import com.squareup.okhttp.MediaType;
import com.squareup.okhttp.OkHttpClient;
import com.squareup.okhttp.Request;
import com.squareup.okhttp.Request.Builder;
import com.squareup.okhttp.RequestBody;
import com.squareup.okhttp.Response;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.concurrent.TimeUnit;

public class WebServiceHelper extends AsyncTask<String, Void, String> {

	private Builder requestBuilder = null;
	private String requestURL = null;
	
	private static final MediaType JSON = MediaType.parse("application/json; charset=utf-8");
	//private static final MediaType OCTET_STREAM = MediaType.parse("application/octet-stream");
	private static final MediaType JAVA_SERIALIZED_OBJECT = MediaType.parse("application/x-java-serialized-object");
	
	OkHttpClient client = new OkHttpClient();
	public AsyncResponseListener delegate = null;
	
	public WebServiceHelper(String requestURL){
		this.requestURL = requestURL;
		this.requestBuilder = new Builder().url(requestURL);
	}

	protected String doInBackground(String... params) {
		try {				
			Request request = requestBuilder.build();
			
			System.out.println("Request URL: " + request.urlString());
			System.out.println("Auth Token: " + request.header("auth_token"));
			
			if(request.body() != null){
				System.out.println("Data Type: " + request.body().contentType().toString());
			}
			
			client.setConnectTimeout(8000, TimeUnit.SECONDS);
			Response response = client.newCall(request).execute();
			//System.out.println("response ==>"+response.body().string());
			return response.body().string();       
		}
		catch (Exception e) {
			
			System.out.println("Connection Exception:" + e.getLocalizedMessage());
			
			//Create the JSON for response
			JSONObject errorResponse = new JSONObject();
		    try {
		    	//Creating JSON object for response_status key
		    	JSONObject response_status = new JSONObject();
		    	response_status.put("response_code", 1);
		    	response_status.put("response_message", "Failed");
		    	
		    	//Creating JSON object for response_data key
		    	JSONObject response_data = new JSONObject();
		    	response_data.put("error", "Unable to complete the request due to network connection error. Please try again.");
		    	
		    	errorResponse.put("response_status", response_status);
		    	errorResponse.put("response_data", response_data);
		    	
			} catch (JSONException JSONexp) {
				JSONexp.printStackTrace();
			}
		    
		    return errorResponse.toString();
	    }
	}

	protected void onPostExecute(String responseString) {
	    // TODO: check this.exception 
	    // TODO: do something with the feed
		System.out.println("Response: " + responseString);
		
		delegate.processFinish(this.requestURL, responseString);
	}
	
	public void addHeader(String name, String value){
		this.requestBuilder.addHeader(name, value);
	}
	
	public void setRequestBody(String requestData){
		System.out.println("Request Body: " + requestData);
		this.requestBuilder.post(RequestBody.create(JSON, requestData));
	}
	
	public void setRequestBody(byte[] requestData){
		System.out.println("Request Body: " + requestData.toString());
		this.requestBuilder.post(RequestBody.create(JAVA_SERIALIZED_OBJECT, requestData));
	}

	


}
