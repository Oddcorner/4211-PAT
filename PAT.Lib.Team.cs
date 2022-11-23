using System;
using System.Collections.Generic;
using System.Text;
using PAT.Common.Classes.Expressions.ExpressionClass;

//the namespace must be PAT.Lib, the class and method names can be arbitrary
namespace PAT.Lib
{
    /// <summary>
    /// The math library that can be used in your model.
    /// all methods should be declared as public static.
    /// 
    /// The parameters must be of type "int", or "int array"
    /// The number of parameters can be 0 or many
    /// 
    /// The return type can be bool, int or int[] only.
    /// 
    /// The method name will be used directly in your model.
    /// e.g. call(max, 10, 2), call(dominate, 3, 2), call(amax, [1,3,5]),
    /// 
    /// Note: method names are case sensetive
    /// </summary>
   	public class Player : ExpressionValue
    {
        private int offense;
        private int defense;
        private int receive;
        
        public Player() {
        	offense = 75;
        	defense = 75;
        	receive = 75;
        }
               
        public Player(int o, int d, int r) {
        	offense = Math.Max(o, 1);
        	defense = Math.Max(d, 1);
        	receive = Math.Max(r, 1);
        }
        
        public int GetOffense() {
        	return offense;
        }
        
        public int GetDefense() {
        	return defense;
        }
        
        public int GetReceive() {
        	return receive;
        }
        
        
        /// <summary>
        /// Please implement this method to provide the string representation of the datatype
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "(" + offense.ToString() + "," + 
           	        defense.ToString() + "," + 
           	        receive.ToString() + ")";
        }


        /// <summary>
        /// Please implement this method to return a deep clone of the current object
        /// </summary>
        /// <returns></returns>
        public override ExpressionValue GetClone()
        {
            return new Player(offense, defense, receive);
        }


        /// <summary>
        /// Please implement this method to provide the compact string representation of the datatype
        /// </summary>
        /// <returns></returns>
        public override string ExpressionID
        {
            get {
            	return this.ToString();
            }
        }

    } 
   
   
   
   
    public class Team : ExpressionValue
    {
		private Player[] team = new Player[6];
		private int RS = 0;
		private int MD = 1;
		private int OH1 = 2;
		private int OH2 = 3;
		private int DS = 4;
		private int S = 5;
		
		public Team() {
		}
		
		public Team(Player rs, Player md, Player oh1, Player oh2, Player ds, Player s) {
			team[RS] = rs; // Defender 1 (RS)
			team[MD] = md; // Defender 2 (MD)
			team[OH1] = oh1; // Defender 3 (OH)
			team[OH2] = oh2; // Attacker (OH)
			team[DS] = ds; // Receiver (DS)
			team[S] = s; // setter (S)
			
		}
		
		public Team(Team t) {
			team[RS] = t.team[RS];
			team[MD] = t.team[MD]; 
			team[OH1] = t.team[OH1]; 
			team[OH2] = t.team[OH2]; 
			team[DS] = t.team[DS]; 
			team[S] = t.team[S]; 
		}
		
		public int GetDefense(int r) {
			return Math.Max(team[(RS + r) % 6].GetDefense(), 
				   Math.Max(team[(MD + r) % 6].GetDefense(), 
							team[(OH1 + r) % 6].GetDefense()));
		}
		
		public int GetOffense(int r, int attacker) {
			var choice = new List<int>{ (RS + r) % 6, (OH1 + r) % 6, (OH2 + r) % 6};
			return team[choice[attacker]].GetOffense();
		}
		
		public int GetReceive(int r) {
			return team[(DS + r) % 6].GetReceive();
		}
		
		public int ReceiveProb(Team t, int r1, int r2, int attacker) {
			return 100 + this.GetReceive(r1) - t.GetOffense(r2, attacker);
		}
		
		public int FailBlockProb(Team t, int r1, int r2, int attacker) {
			return 100 + t.GetOffense(r2, attacker) - this.GetDefense(r1);
		}
		
		public int SuccessBlockProb(Team t, int r1, int r2, int attacker) {
			return 100 + this.GetDefense(r1) - t.GetOffense(r2, attacker);
		}

		
		
        /// <summary>
        /// Please implement this method to provide the string representation of the datatype
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
        	string returnString = "";
        	foreach (Player player in team) {
        		returnString += player.ToString() + "|";
        	}
            return returnString;
        }


        /// <summary>
        /// Please implement this method to return a deep clone of the current object
        /// </summary>
        /// <returns></returns>
        public override ExpressionValue GetClone()
        {
            return new Team(this);
        }


        /// <summary>
        /// Please implement this method to provide the compact string representation of the datatype
        /// </summary>
        /// <returns></returns>
        public override string ExpressionID
        {
            get {return this.ToString(); }
        }

    }
}