import { nanoid } from 'nanoid';

class User {
    constructor(userName, primaryLevel){
        this.id = nanoid();
        console.log(this.id);
        this.userName = userName;
        this.primaryLevel = primaryLevel;
    }
}

export {User};