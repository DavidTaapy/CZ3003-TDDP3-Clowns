import {firestore} from "../model/db.js";

//add questions
const addQuestions = async(req, res) => {
    try {
        const qn = req.body;
        const questiondb = firestore.collection('questions');    
        questiondb.add(qn);
        res.send("question added!");
    } catch (error) {
        res.status(400).send("error adding questions!");
    }
};

// Get questions by primary level
const getQuestions = async(req, res) => {
    try {
        if (req.query.lvl == null || req.query.lvl == ""){
            res.status(400).send("Missing lvl param in request!");
        }
        if (parseInt(req.query.lvl) > 6 || parseInt(req.query.lvl) < 1){
            res.status(400).send("primaryLevel out of range!");
        }
        const lvl = req.query.lvl;
        const questiondb = firestore.collection('questions');
        const snapshot = await questiondb.where('primaryLevel', '==', parseInt(lvl)).get();
        res.send(snapshot.docs.map(qn => qn.data()));

    } catch (error) {
        return res.status(400).send(error.message);
    }
};

// detele qn
const deleteQuestion = async(req, res) => {
    try {
        const qn = req.query.qn;
        var itemsdb = firestore.collection('questions');
        itemsdb.where('question', '==', qn).get().then(function(snapshot){
            if (!snapshot.empty) {
                snapshot.forEach(function(doc) {
                    doc.ref.delete();
                });
                return res.send("question is deleted!");
            } else {
                return res.status(400).send("No such question!");
            }
        });
    } catch (error) {
        return res.status(400).send("Missing qn param!");
    }
};



export {getQuestions, addQuestions, deleteQuestion};