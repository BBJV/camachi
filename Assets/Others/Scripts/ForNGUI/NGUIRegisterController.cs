using UnityEngine;
using System;
using System.Collections;

public class NGUIRegisterController : MonoBehaviour {

	private enum ProcessState {
		FREE,
		PREPARE,
		START,
		CONNECTING,
		CONNECTED,
		REGISTERING,
		REGISTERED,
		FAIL,
		FAIL_USEREXIST,
		FAIL_PLAYEREXIST,
		FAIL_NOTINPUTALL,
		FAIL_PASSWORDCONFIRMWRONG,
		UNAVAILABLE,
		FINISH,
		MASTERSERVERFAIL,
		CONNECTIONFAIL
		
	}
	
	private NGUIRegisterController.ProcessState processState = NGUIRegisterController.ProcessState.FREE;

//	private string registerErrorMessage = "";
	
	public string registrationServiceIP = "127.0.0.1";
	public int registrationServicePort = 50000;	

	public int connectToGameLobbyCount = 0;
	public int maxConnectToGameLobbyCount = 0;
	
	public NGUIRegisterSceneViewer registerSceneViewer;
	
	public int MIN_LENGTH =  8 ;
	public int MAX_LENGTH = 16 ;
	
	#region RPC
	
	[RPC] //Define for client's RPC
	private void SendToGameLobby_Register(string registerMemberID, string registerPassword, string registerNickName, string Email, string photoName ) {
		
	}
	 
	[RPC]
	public void ReceiveByClientPortal_Register(int p) {
		
//		Debug.Log ("ReceiveByClientPortal_Register");
		Definition.RPCProcessState resultState = (Definition.RPCProcessState)p;
		Network.Disconnect();
		
		if(this.processState == NGUIRegisterController.ProcessState.REGISTERING) {
			
			if(resultState == Definition.RPCProcessState.SUCCESS) {
				this.processState = NGUIRegisterController.ProcessState.REGISTERED;
				
				registerSceneViewer.RegisterSuccessSceneSetting();
				
			} else if(resultState == Definition.RPCProcessState.USEREXIST) {
				this.processState = NGUIRegisterController.ProcessState.FAIL_USEREXIST;
				
				registerSceneViewer.RegisterAccountErrorSceneSetting();
				
			} else if(resultState == Definition.RPCProcessState.PLAYEREXIST) {
				this.processState = NGUIRegisterController.ProcessState.FAIL_PLAYEREXIST;
				
				registerSceneViewer.RegisterNickNameErrorSceneSetting();
				
			} else if(resultState == Definition.RPCProcessState.FAIL) {
				this.processState = NGUIRegisterController.ProcessState.FAIL;
				
				registerSceneViewer.RegisterFailSceneSetting();
			} else if(resultState == Definition.RPCProcessState.UNAVAILABLE) {
				this.processState = NGUIRegisterController.ProcessState.UNAVAILABLE;
				
				registerSceneViewer.RegistrationUnavailableSceneSetting();
			}
		}
	}
	
	#endregion
	
	public void Register() {
		
		if((registerSceneViewer.regAccText3DClick.text == "") || 
		   (registerSceneViewer.regPwText3DClick.text == "") || 
		   (registerSceneViewer.regPwConfirmText3DClick.text == "")|| 
		   (registerSceneViewer.regEMailText3DClick.text =="") || 
		   (registerSceneViewer.regNickNameText3DClick.text == "") || 
		   (registerSceneViewer.regAccText3DClick.text == registerSceneViewer.regAccText3DClick.strBaseText)|| 
		   (registerSceneViewer.regPwText3DClick.text == registerSceneViewer.regPwText3DClick.strBaseText)|| 
		   (registerSceneViewer.regPwConfirmText3DClick.text == registerSceneViewer.regPwConfirmText3DClick.strBaseText)|| 
		   (registerSceneViewer.regEMailText3DClick.text == registerSceneViewer.regEMailText3DClick.strBaseText)|| 
		   (registerSceneViewer.regNickNameText3DClick.text == registerSceneViewer.regNickNameText3DClick.strBaseText)
		   
		   ) {
			
//				this.registerErrorMessage = "Please fill in all the text fields.";
				registerSceneViewer.FillInAlltheTextFieldsSceneSetting();
			
			}else if(registerSceneViewer.regPwText3DClick.text != registerSceneViewer.regPwConfirmText3DClick.text) {
				registerSceneViewer.DoubleCheckPasswordSceneSetting();
//				this.registerErrorMessage = this.registerErrorMessage + "Please double check the password is match or not.";
			}else if(!IsAllLettersOrDigits(registerSceneViewer.regAccText3DClick.text) || 
					 !IsAllLettersOrDigits(registerSceneViewer.regNickNameText3DClick.text)) {
				registerSceneViewer.NotAllLettersOrDigitsErrorSceneSetting();
//				this.registerErrorMessage = this.registerErrorMessage + "Please double check the password is match or not.";
			}else if(!IsEmailValid(registerSceneViewer.regEMailText3DClick.text)) {
				registerSceneViewer.EmailIsNotValidErrorSceneSetting();
			}else {
				RegisterStart();
			}
	}
	
	public bool IsAllLettersOrDigits(string s) {
        foreach (char c in s) {
            if (!Char.IsLetterOrDigit(c))
                return false;
        }
        return true;
    }
	
	private int CheckChars(string s, int i, int l) {
//		Debug.Log ("l:"+l.ToString());
		while ( (i < l) && ("_-abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".IndexOf(s[i]) != -1)){  
            i++;  
//			Debug.Log ("i:"+i.ToString());
        }  
//		Debug.Log (i.ToString());
        return i;  
	}
	
	private bool CheckFirstLevelDomainChars(string s, int i, int l) {
		while (i<l && ("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".IndexOf(s[i]) != -1)) {  
            i++;  
        }  
        return (i == l);  
	}
	
	private bool IsEmailValid(string emailAddress) {
//		String.
//		string strPattern = "^([0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
//
//        if ( System.Text.RegularExpressions.Regex.IsMatch(EmailAddress, strPattern) ) {
//			return true; 
//		}
		int i = 0;
		int j = 0;
		int l = emailAddress.Length;    
		bool foundPoint = false;  
		
	    // every email starts with a string  
	    if (( i = CheckChars(emailAddress, 0, l)) == 0) {  
	        return false;  
	    }  
//		Debug.Log ("Pass check : every email starts with a string  ");
		//init j  .

		j = i;  
		  
		// followed by an arbitrary number of ("." string) combinations  
		while ( i < l && emailAddress[i-1] == '.') {  
		    // skip the point  
		    i++;  
		    // if there are no chars, we have an error  
		    if ((j = CheckChars(emailAddress, i, l)) == i) {  
		        return false;  
		    }  
		    // else skip the chars  
		    i = j;  
		    
		}  
//			Debug.Log ("Pass check :  followed by an arbitrary number of (\".\" string) combinations  ");
		
		// then follows the magic @ 
//		Debug.Log (i.ToString());
		
		if(i >= emailAddress.Length ) {
			return false;  
		}
		
//		Debug.Log ("emailAddress[i]:"+emailAddress[i]);
		if (emailAddress[i] != '@'){  
		    //trace(emailAddress.charAt(i));  
		    return false;  
		}  
		  
		// followed by minimum one string point string  
		// after the last point minimum 2 characters are allowed  
//		 Debug.Log ("Pass check @");
		do {  
		    // skip the @ (j == i at the beginning, so it is like i++) 
			
		    i = j + 1;  
		    //trace(emailAddress.substring(i));  
		    // do we have more chars ?  
			
//			Debug.Log ("emailAddress[i]:"+emailAddress[i]);
			
		    j = CheckChars(emailAddress, i, l);  
		    if (j == i) {  
		        // no more chars found -> error
//				Debug.Log ("no more chars found -> error");
		        return false;  
		    } else if (j == emailAddress.Length) {  
		        //trace("j==e.length");  
		        // emailaddress is finished, do we have a first level domain ?  
		        j -= i;  
		        // we have one if it is at least 2 long and consists of the correct characters  
		        if(foundPoint && j >= 2 && CheckFirstLevelDomainChars(emailAddress, i, l)){  
//		            Debug.Log ("Email OK");
					return true;
					
		        } else { 
//					Debug.Log ("we have one if it is at least 2 long and consists of the correct characters");
		            return false;
		        }  
		    }  
		    // if we reach the end or don't have a point, we return an error  
//			Debug.Log ("emailAddress[j]:"+emailAddress[j]);
		    foundPoint = (emailAddress[j] == '.');  
		} while (i<l && foundPoint);  
		
//		Debug.Log ("EndError");
		return false;
	}
	
	
//	private bool IsPasswordValid(string password) {
//
//////		string strPattern = "^.*(?=.{1,20})(?=.*[a-zA-Z])(?=.*\\d)(?=.*[!#$%&? \"]).*$";
////		string strPattern = "^.*(?=.{,20})([a-zA-Z]*)(\\d*)([-_+=/*^@<>\\!.,:;'/#$%&? \"]*).*$";
////
////        if ( System.Text.RegularExpressions.Regex.IsMatch(password, strPattern) ) {
////			return true; 
////		}
////		return false;
//		
//		if(password.Length >= MIN_LENGTH && password.Length <= MAX_LENGTH){
//			// match length
//			foreach(Char c in password) {
//				if(!Char.IsLetterOrDigit(c)) {
//					
//					Debug.Log (c + " is not letter or digit");
//					
//					if(!Char.IsSymbol(c)) {
////						Debug.Log (c + " is not symbol");
//						return false;
//					}
//				}
//			}
//			Debug.Log (password + " OK");
//			return true;
//			
//		}else{
//			
//			Debug.Log (password + "is not match number");
//			return false;
//		}
//		
//		
//		
//	}

	private void RegisterStart() {
		this.processState = NGUIRegisterController.ProcessState.START;
		
		if(Network.peerType == NetworkPeerType.Disconnected) {
			Network.Connect(registrationServiceIP, registrationServicePort);
			this.processState = NGUIRegisterController.ProcessState.CONNECTING;
			registerSceneViewer.ConnectingToServerSceneSetting();
		}else{
			registerSceneViewer.RegisterErrorSceneSetting();
//			Debug.Log("FAIL");
			this.processState = NGUIRegisterController.ProcessState.FAIL;
		}
		
	}
	
	void OnLevelWasLoaded(int mapIndex) {
		if(mapIndex == (int)Definition.eSceneID.LoginScene_ngui) {
			
			registerSceneViewer = GameObject.FindObjectOfType(typeof(NGUIRegisterSceneViewer)) as NGUIRegisterSceneViewer;
			
		}
//		Debug.Log("Register:OnLevelWasLoaded");
	}
	
	void OnConnectedToServer() {
//		Debug.Log ("OnConnectedToServer");
		if(this.processState == NGUIRegisterController.ProcessState.CONNECTING) {
			
			registerSceneViewer.ConnectedToServerSceneSetting();
			
			this.processState = NGUIRegisterController.ProcessState.CONNECTED;
	        networkView.RPC("SendToGameLobby_Register", RPCMode.Server, registerSceneViewer.regAccText3DClick.text, registerSceneViewer.regPwText3DClick.text, registerSceneViewer.regNickNameText3DClick.text, registerSceneViewer.regEMailText3DClick.text, registerSceneViewer.strRegisterPhoto);
			this.processState = NGUIRegisterController.ProcessState.REGISTERING;
			
			
//			Debug.Log ("acc:"+registerSceneViewer.regAccText3DClick.text +", pass:"+ registerSceneViewer.regPwText3DClick.text +", nick name:"+ registerSceneViewer.regNickNameText3DClick.text +", email:"+ registerSceneViewer.regEMailText3DClick.text +", photo:"+ registerSceneViewer.strRegisterPhoto);
			registerSceneViewer.RegisteringSceneSetting();
		}
	}
	
	void OnFailedToConnect() {
//		Debug.Log ("OnFailedToConnect");
		if(this.processState == NGUIRegisterController.ProcessState.CONNECTING) {
			if(connectToGameLobbyCount-- > 0) {
				RegisterStart();
			}else{
				this.processState = NGUIRegisterController.ProcessState.CONNECTIONFAIL;
				registerSceneViewer.FailedToConnectErrorSceneSetting();
				connectToGameLobbyCount = maxConnectToGameLobbyCount;
			}
		}
	}
}
