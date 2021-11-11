import {firestore} from "../model/db.js";

//add restaurant and dish
const addRes = async(req, res) => {
    try {
        const game = req.body;
        const resdb = firestore.collection('restaurant');    
        await resdb.add(game).then(function(docRef) {
            resdb.doc(docRef.id).set({"id": docRef.id}, { merge: true });
        });
        return res.send("restaurant added!");
    } catch (error) {
        res.status(400).send(error.message);
        res.send("error adding");
    }
};

// Get restaurant by selection
const getRes = async(req, res) => {
    try {
        const name = req.query.name;
        const resdb = firestore.collection('restaurant');
        resdb.where('name', '==', name).get().then(function(snapshot){
            if (!snapshot.empty) {
                res.contentType('application/json');
                res.send(snapshot.docs.map(currRes => currRes.data()));
            } else {
                return res.status(400).send("No such restaurant!");
            }
        });
    } catch (error) {
        res.status(400).send("Missing restaurant name param");
    }
};

const deleteRes = async(req, res) => {
    try {
        const resName = req.query.name;
        var itemsdb = firestore.collection('restaurant');
        itemsdb.where('name', '==', resName).get().then(function(snapshot){
            if (!snapshot.empty) {
                snapshot.forEach(function(doc) {
                    doc.ref.delete();
                });
                return res.send("Restaurant is deleted!");
            } else {
                return res.status(400).send("No such restaurant!");
            }
        });
    } catch (error) {
        return res.status(400).send("Missing restaurant name param!");
    }
};

export {getRes, addRes, deleteRes};