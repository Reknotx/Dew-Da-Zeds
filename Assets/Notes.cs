public class Notes
{
    /*
     * 1. Following the idea of the tower defense template I should create my own two
     * frameworks that mimic it in a way.
     * 
     * 2. The main concepts that are seen in the tower defense framework are seen elsewhere 
     * in other action games as well as similar tower defense games
     * 
     * 3. There is the action game framework that includes code which covers the concepts 
     * of taking damage and handling the logic for ballistics and projectiles.
     * 
     *      - Scripts that manage health when damage is taken.
     *      
     *      - Scripts that handle audio when turrets are fired, enemies are hit by turrets,
     *      health is lost, when enemies die, game music, etc.
     *      
     *      - Scrips which handle the projetile and ballistics such as how fast they move, how
     *      long they are being rendered, where they are being fired too, etc.
     *      
     *      - 
     * 
     * 4. The core framework has code which handles all common aspects to games such as saving,
     * data management, timers, math utilities, and more.
     * 
     *      - Timers: will handle advancing small clocks that are stored in code and then issueing
     *      callbacks to the Actions that are attached to the timers, or the functions that we wish
     *      to invoke. I.e. firing a projectile at a nearby enemy on the screen.
     *      
     *      - Saving: will handle saving the game's data and state when the player closes the game
     *      and wishes to restart at that same place when they next reopen up the game on their
     *      phone.
     *      
     *      - Data management: will handle managing the data of the game such as economy, lives,
     *      score, time elapsed since game start, game settings such as sound effects and music
     *      volume, etc.
     *      
     *      - Math utilities: will help perform common math formulas that are consistent throughout
     *      the entire game's code and which are used frequently by multiple scripts
     *      
     * 5. The timer class will be the most crucial class that I use for the game, all turrets will
     * have timer objects attached to them which will be called every Update in game and given a 
     * delta time which will advance an internal value. If that value is greater than the fire rate
     * then we will call an invoke on the Action and execute the function.
     * 
     * 6. How do we want targeting to take place in the game with the towers? What are some ideas?
     * 
     *      - Cast 
     */
}
