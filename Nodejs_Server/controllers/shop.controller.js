import {firestore} from "../model/db.js";

//add items
const addItems = async(req, res) => {
    try {
        const type = req.query.type;
        const items = req.body;
        const shopdb = firestore.collection('shop'); 
        var itemUpdate = {};
        itemUpdate[`${type}`] = items;   
        await shopdb.doc('items').set(itemUpdate, { merge: true });
        res.send("items added");
    } catch (error) {
        res.status(400).send(error.message);
        res.send("error adding");
    }
};
// Get all items in shop
const getItems = async(req, res) => {
    try {
        const shopdb = firestore.collection('shop');
        const allItems = await shopdb.get();
        res.send(allItems.docs.map( item=> item.data()));

    } catch (error) {
        res.status(400).send(error.message);
        res.send("error getting items!");
    }
};




export {getItems, addItems};