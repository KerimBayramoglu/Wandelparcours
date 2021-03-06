/**
 * scans all the beacons and returns the closest beacon
 */
exports.scanClosest = function () {
    return new Promise(function (resolve) {
        scanner().then(function (scannedData) {
            let el=null;
            scannedData.forEach(element => {
                if(el==null){
                    el={id:element.minor,dist:element.rssi};
                }
                if(element.rssi > el.rssi){
                    el={id:element.minor,dist:element.rssi};
                }
            });
            if(el!=null){
                resolve(el.id);
            }else{
                resolve(null);
            }
        }).catch(function () {
            resolve([])
        });

    });
}
/**
 * scans all beacons
 */
function scanner() {
    return new Promise((resolve) => {
        try {
            const Bleacon = require("bleacon");
            beacons = [];
            Bleacon.startScanning();
            Bleacon.on('discover', function (bleacon) {
                beacons.push(bleacon)
            });
            setTimeout(()=>{
                resolve(beacons);
            },3000)
        } catch (e) {
            console.log(e);
            resolve([])}
    });

}

/*
                    }



                    return new Promise(function (presolve) {
                        setTimeout(function () {
                            var jsonbeacons = [];
                            beacons.forEach(function (val, key, map) {
                                jsonbeacons.push({
                                    id: key,
                                    rssi: val.rssi,
                                    beacon:val
                                })
                            });
                            presolve(jsonbeacons);
                        }, 4000);
    
                    });
                }
            } catch (error) {
                resolve("");
            }

        });
    }

}*/