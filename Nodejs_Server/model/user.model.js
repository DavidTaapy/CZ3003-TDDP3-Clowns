class User {
    constructor(userName, primaryLevel){
        this.userName = userName;
        this.primaryLevel = primaryLevel;
        this.eloRating = 0;
        this.points = 0;
        //this.coins = 0;
        this.inventory = [];
    }
}

export {User};