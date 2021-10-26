class User {
    constructor(id, userName, primaryLevel){
        this.id = id;
        this.userName = userName;
        this.primaryLevel = primaryLevel;
        this.eloRating = 0;
        this.points = 0;
        this.inventory = [];
        this.completedQns = [];
        this.correctQns = 0;
        this.wrongQns = 0;
        this.character = {
            characterName: "Chef Jar",
            characterDescription: "I am a jar!",
            characterID: "Wx7ePiSen9NjEL0LtxoK", 
            spriteSource: ""
        };
    }
}

export {User};