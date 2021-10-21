class User {
    constructor(id, userName, primaryLevel){
        this.id = id;
        this.userName = userName;
        this.primaryLevel = primaryLevel;
        this.eloRating = 0;
        this.points = 0;
        this.inventory = [];
        this.questions = [];
        this.correctQns = 0;
        this.wrongQns = 0;
    }
}

export {User};