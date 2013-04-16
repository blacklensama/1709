package processEngine.business;

import java.io.Serializable;

public class User implements Serializable{

	private String name;
	private int id;
	private enum Gender {male,female}
	private Gender gender ;
	
	private String email;
	private String role;
	private String group;
	public User(String name,String email,String group){
		this.name = name;
		this.email = email;
		this.group = group;
	}
	
	public User(String name,String email){
		this.name = name;
		this.email = email;
	}
	public String getName() {
		return name;
	}
	public void setName(String name) {
		this.name = name;
	}
	public int getId() {
		return id;
	}
	public void setId(int id) {
		this.id = id;
	}
	public Gender getGender() {
		return gender;
	}
	public void setGender(Gender gender) {
		this.gender = gender;
	}
	public String getEmail() {
		return email;
	}
	public void setEmail(String email) {
		this.email = email;
	}
	public String getRole() {
		return role;
	}
	public void setRole(String role) {
		this.role = role;
	}
	public String getGroup() {
		return group;
	}
	public void setGroup(String group) {
		this.group = group;
	}
	
}
