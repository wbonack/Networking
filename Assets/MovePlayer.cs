using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour {
	public float moveSpeed = 25;
	public float turnSpeed = 3;
	public float xCoord = 0;
	public float yCoord = 0;
	public int playerScore = 0;
	public bool currentPlayer = false;
	public GameObject mainCamera;
	public GameObject secondCamera;
	public bool changedRecently = false;
    public int playerNumber;
	int updateCounter = 0;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (currentPlayer)
		{
			// Move player forward
			float playerMoveAmount = moveSpeed * Input.GetAxis("Vertical");
            
			this.rigidbody.AddRelativeForce(playerMoveAmount,0, 0);
			
			// Turn player left or right
			float playerTurnAmount = Input.GetAxis("Horizontal");
			transform.Rotate(0, playerTurnAmount * turnSpeed, 0);
			
			xCoord = rigidbody.position.x;
			yCoord = rigidbody.position.y;

            // Send the information over the network
            if (playerMoveAmount > 0 || playerTurnAmount > 0)
            {
                print("x: " + xCoord + " y: " + yCoord);
            }

            
            string serverName = GameObject.Find("Plane").GetComponent<MultiplayerScript>().serverName;
            NetworkView networkView = this.networkView;

            if (GameObject.Find("Plane").GetComponent<MultiplayerScript>().isNetworked)
            {
                // this.GetComponent<MultiplayerScript>().networkView.RPC("PrintGamePosition", RPCMode.AllBuffered, serverName);
            }
            //this.GetComponent<MultiplayerScript>().RPC("PrintGamePosition", networkView, serverName);
			
			if (Input.GetKeyDown(KeyCode.P))
			{
				bool changedCamera = false;
                GameObject go = GameObject.Find("Player");
				GameObject go2 = GameObject.Find("Player2");
				
				//if (this.tag == go.tag && changedCamera == false && changedRecently == false)
                if (this.tag == go.tag && changedCamera == false )
                {
					print ("p1");
					
		//			go.GetComponent<MovePlayer>().setActive(false);
		//			go2.GetComponent<MovePlayer>().setActive(true);
					
					changedCamera = true;
				}
				
				//if (this.tag == go2.tag && changedCamera == false && changedRecently == false)
                if (this.tag == go2.tag && changedCamera == false )
				{	
					print ("p2");
			//		go2.GetComponent<MovePlayer>().setActive(false);
			//		go.GetComponent<MovePlayer>().setActive(true);
					
					changedCamera = true;
				}
				
				// Make sure a keypress doesn't get called again in the next second
                /*
				if (changedRecently == true)
				{
                    print("changed recently");
					updateCounter++;
					if (updateCounter > 1)
					{
						changedRecently = false;
						updateCounter = 0;
					}
				}
                */
			}
		}
	
	}
	
	void OnTriggerEnter(Collider other) {
		if (currentPlayer)
		{
			print("Other tag: " + other.tag);
			if (other.tag == "Companion Cube"){
				print("You hit the innocent companion cube.");			
			}
			Destroy(other.gameObject);
			
			playerScore += 10;
			print("Your score now is: " + playerScore.ToString());
		}
    }
	
	
	
	void OnMouseDown(){
		print("Player clicked, his tag: " + this);
	}
		
	/*		
	public void setActive(bool activeValue)
	{
		currentPlayer = activeValue;
		if (activeValue == true)
		{
			this.mainCamera.active = true;
			this.secondCamera.active = false;
		}
		
	}
     * */

    public bool getCurrent()
    {
        return currentPlayer;
    }
	
}
