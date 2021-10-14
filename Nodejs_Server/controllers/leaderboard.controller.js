import {firestore} from "../model/db.js";

// Get leaderboard (sort and filter for top) - used 2 in this case
const getLeaderboard = async(req, res) => {
    try {
        let temp = {'userName': 'username', 'eloRating': 0};
        const results = [];
        const userdb = firestore.collection('users');    
        const snapshot = await userdb.orderBy('eloRating', 'desc').get(); // removed .limit(2)
        const ranks = snapshot.docs.map(user => user.data());
        /*ranks.forEach((user) => {
            temp.userName = (user.userName); 
            temp.eloRating = (user.eloRating); 
            results.push(temp);
        });
        console.log(results);*/
        res.send(ranks);

    } catch (error) {
        res.status(400).send(error.message);
        res.send("error getting users!");
    }
};


export {getLeaderboard};