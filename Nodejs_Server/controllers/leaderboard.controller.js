import {firestore} from "../model/db.js";

// Get leaderboard (sort and filter for top) - used 2 in this case
const getLeaderboard = async(req, res) => {
    try {
        const userdb = firestore.collection('users');
        const snapshot = await userdb.get();
        //const currLeaderboard = await allUsers.doc().orderBy('eloRating').limit(2).get()
        res.send(snapshot.docs.map(doc => doc.data().id));
        //res.send(allUsers.data());
    } catch (error) {
        res.status(400).send(error.message);
        res.send("error adding user!");
    }
};


export {getLeaderboard};