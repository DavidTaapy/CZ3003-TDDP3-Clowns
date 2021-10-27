import {firestore} from "../model/db.js";

// Get leaderboard (sort and filter for top) - used 2 in this case
const getLeaderboard = async(req, res) => {
    try {
        let temp = {'userName': 'username', 'eloRating': 0};
        const results = [];
        const userdb = firestore.collection('users');    
        const snapshot = await userdb.orderBy('eloRating', 'desc').get(); // removed .limit(2)
        const ranks = snapshot.docs.map(user => user.data());
        res.send(ranks);

    } catch (error) {
        res.status(400).send(error.message);
        res.send("error getting users!");
    }
};

const getPastLeaderboard = async(req, res) => {
    try {
        if (req.query.seasonId == null || req.query.seasonId == ""){
            console.log(req.query.seasonId);
            res.status(400).send("Missing seasonId param in request!");
        }
        const id = req.query.seasonId;
        var pastLeaderboarddb = firestore.collection('pastLeaderboard');
        pastLeaderboarddb = pastLeaderboarddb.where('seasonID', '==', parseInt(id));
        const snapshot = await pastLeaderboarddb.get();
        res.contentType('application/json');
        res.send(snapshot.docs.map( item=> item.data())[0]);
    } catch (error) {
        res.status(400).send("Error in retrieving past leaderboard");
    }
};


export {getLeaderboard, getPastLeaderboard};