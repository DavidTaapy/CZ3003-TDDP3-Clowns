import chai from "chai";
import chaiHttp from "chai-http";
import {server} from "../app.js";
var expect = chai.expect;

//Assertion Style
chai.should();

chai.use(chaiHttp);

describe('Testing Item API (item.controller.js)', () => {

    var new_item ={
        "itemName" : "Knife",
        "price" : 0,
        "itemType" : "Accessory",
        "itemSource" : "Shop",
    }
    
    //Test GET Shop powerup
    describe('GET Shop powerup with 2 valid params', () => {
        it("This should get all powerups in shop", (done) => {
            chai.request(server)
            .get("/items")
            .query({itemSource: "Shop", itemType: "Powerup"})
            .end((err, res) => {
                expect(res).to.have.status(200);
                expect(res).to.be.json;
                expect(res.body[0]).to.have.property('itemName');
                expect(res.body[0]).to.have.property('price');
                expect(res.body[0]).to.have.property('spriteSource');
                expect(res.body[0]).to.have.property('itemSource').eq("Shop");
                expect(res.body[0]).to.have.property('itemType').eq("Powerup");
            done();
            })
        })
    })

    //Test GET leaderboard accessories
    describe('GET leaderboard accessories with 2 valid params', () => {
        it("This should get all accessories on the leaderboard", (done) => {
            chai.request(server)
            .get("/items")
            .query({itemSource: "Leaderboard", itemType: "Accessory"})
            .end((err, res) => {
                expect(res).to.have.status(200);
                expect(res).to.be.json;
                expect(res.body[0]).to.have.property('itemName');
                expect(res.body[0]).to.have.property('price');
                expect(res.body[0]).to.have.property('spriteSource');
                expect(res.body[0]).to.have.property('itemSource').eq("Leaderboard");
                expect(res.body[0]).to.have.property('itemType').eq("Accessory");
            done();
            })
        })
    })

    //Test GET shop powerup with missing itemType param
    describe('GET shop powerup with missing itemType param', () => {
        it("This should get an error message for missing itemType param", (done) => {
            chai.request(server)
            .get("/items")
            .query({itemSource: "Shop"})
            .end((err, res) => {
                expect(res).to.have.status(400);
                res.text.should.be.eq("Missing itemType param in request!");
            done();
            })
        })
    })

    //Test GET shop powerup with missing itemSource param
    describe('GET shop powerup with missing itemSource param', () => {
        it("This should get an error message for missing itemSource param", (done) => {
            chai.request(server)
            .get("/items")
            .query({itemType: "Accessory"})
            .end((err, res) => {
                expect(res).to.have.status(400);
                res.text.should.be.eq("Missing itemSource param in request!");
            done();
            })
        })
    })

    //Test POST new item into the shop
    describe('POST new item into the shop', () => {
        it("This should add a item into the shop", (done) => {
            chai.request(server)
            .post("/items")
            .type("JSON")
            .send(new_item)
            .end((err, res) => {
                expect(res).to.have.status(200);
                res.text.should.be.eq("item added!");
            done();
            })
        })
    })

    // Test DELETE item that does not exist
    describe('DELETE item that does not exist', () => {
        it("This should not delete any items and return an error message", (done) => {
            chai.request(server)
            .delete("/items")
            .query({name : "Knife1"})
            .end((err, res) => {
                expect(res).to.have.status(400);
                res.text.should.be.eq("No such item!");
            done();
            })
        })
    })

    // Test DELETE existing item
    describe('DELETE existing item in the items database', () => {
        it("This should delete an existing item", (done) => {
            chai.request(server)
            .delete("/items")
            .query({name : "Knife"})
            .end((err, res) => {
                expect(res).to.have.status(200);
                res.text.should.be.eq("item is deleted!");
            done();
            })
        })
    })

})