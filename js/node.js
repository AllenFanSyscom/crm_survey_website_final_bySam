
var fs = require('fs');

var path = require('path');

const json2xls = require('json2xls');

console.log('path.resolve(__dirname)', path.resolve(__dirname, '../'));

 

fs.writeFile(path.join(__dirname,"output2.txt"), "Hello World!", function(err) {

    if(err) {

        return console.log(err);

    }

    console.log("File saved successfully!");

});

 

fs.readFile('config.js','utf8',(err,data)=>{

    let parseData = JSON.parse(data);

    let json = [];

    for (const key in parseData) {

        if (parseData[key].pageId) {

            json.push({

                pageId: parseData[key].pageId,

                pageName: parseData[key].pageName,

            })

        }

    }

    if (err) throw err;

    const jsonArray = [];

    json.forEach(function(item){

      let temp = {

        'pageId': item.pageId,

        'pageName': item.pageName,

      }

      jsonArray.push(temp);

    });

     

    let xls = json2xls(json);

    fs.writeFileSync('name.xlsx', xls, 'binary');

  })