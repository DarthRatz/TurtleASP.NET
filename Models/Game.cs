using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Turtle.Models
{
    public class Game{
        public Grid grid { get; set; }
        public Turtle turtle { get; set; }
        public Exit exit { get; set; }
        public List<Mine> mines { get; set; }
        public bool gameWon = false;
        public bool gameLost = false;

        public Game(){
        }

        public Game(string filename)
        {
            Game Game = null;
            XmlSerializer serializer = new XmlSerializer(typeof(Game));
            using (Stream reader = new FileStream(filename, FileMode.Open))
            {
                Game = (Game)serializer.Deserialize(reader);          
            }

            this.grid = Game.grid;            
            this.turtle = Game.turtle;
            this.exit = Game.exit;
            this.mines = Game.mines;
        }

        public override string ToString(){
            return "width:" + grid.width.ToString() + " height:" + grid.height.ToString();
        }

        public bool CheckOutOfBounds(){
            if (this.turtle.position.X > this.grid.width || this.turtle.position.X < 0 
            || this.turtle.position.Y > this.grid.height || this.turtle.position.Y < 0){
                this.gameLost = true;
                return true;
            }

            return false;
        }

        public bool CheckReachedExit(){
            if (this.turtle.position.Equals(this.exit.position)){
                this.gameWon = true;
                this.exit.reached = true;
                return true;
            }

            return false;
        }

        public bool CheckHitMine(){
            foreach(Mine mine in this.mines){
                if (this.turtle.position.Equals(mine.position)){
                    this.gameLost = true;
                    mine.detonated = true;
                    return true;
                }
            }

            return false;
        }

        public Game DeserializeGrid(string filename)
        {
            Game Game = null;
            XmlSerializer serializer = new XmlSerializer(typeof(Game));
            using (Stream reader = new FileStream(filename, FileMode.Open))
            {
                Game = (Game)serializer.Deserialize(reader);          
            }

            return Game;
        }
    }   
}
