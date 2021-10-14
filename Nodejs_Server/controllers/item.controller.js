import {firestore} from "../model/db.js";

//add items
const addItems = async(req, res) => {
    try {
        const items = req.body;
        const itemsdb = firestore.collection('items'); 
        itemsdb.add(items).then(function(docRef) {
            itemsdb.doc(docRef.id).set({"id": docRef.id}, { merge: true });
        });
        res.send("items added");
    } catch (error) {
        res.status(400).send(error.message);
        res.send("error adding");
    }
};
// Get all items in shop
const getItems = async(req, res) => {
    try {
        const source = req.query.itemSource;
        const type = req.query.itemType;
        var itemsdb = firestore.collection('items');
        itemsdb = itemsdb.where('itemSource', '==', source);
        itemsdb = itemsdb.where("itemType", "==", type);
        const snapshot = await itemsdb.get();
        res.send(snapshot.docs.map( item=> item.data()));

    } catch (error) {
        res.status(400).send(error.message);
        res.send("error getting items!");
    }
};




export {getItems, addItems};