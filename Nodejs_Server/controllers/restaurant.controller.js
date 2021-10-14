import {firestore} from "../model/db.js";

//add restaurant
const addRes = async(req, res) => {
    try {
        const game = req.body;
        const resdb = firestore.collection('restaurant');    
        resdb.add(game);
        res.send("res added");
    } catch (error) {
        res.status(400).send(error.message);
        res.send("error adding");
    }
};

/* //add dish
const addDish = async(req, res) => {
    try {
        //TODO: change if need, not correct
        const res = req.body;
        const shopdb = firestore.collection('shop'); 
        var itemUpdate = {};
        itemUpdate[`${type}`] = items;   
        await shopdb.doc('items').set(itemUpdate, { merge: true });
        res.send("items added");
    } catch (error) {
        res.status(400).send(error.message);
        res.send("error adding");
    }
};*/

// Get restaurant by selection
const getRes = async(req, res) => {
    try {
        const lvl = req.query.lvl;
        const questiondb = firestore.collection('questions');
        //todo: try to randomize qns to get    
        const snapshot = await questiondb.where('primaryLevel', '==', lvl).get();
        res.send(snapshot.docs.map(qn => qn.data()));

    } catch (error) {
        res.status(400).send(error.message);
        res.send("error getting questions!");
    }
};




export {getRes, addRes, addDish};