import { baseUrl } from "../contants/contants";

export const getConfiguration = () => {
    return fetch(`${baseUrl}/configuration`)
        .then((response) => { 
            return response.json().then((data) => {
                return data;
            }).catch((err) => {
                console.log(err);
                throw Error("No configuration")
            }).catch(() => {
                throw Error("No configuration")
            })
        });
}

export const postConfiguration = (configurationUpdated) => {
    const options = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json;charset=UTF-8'
        },
        mode: 'cors',
        body: JSON.stringify(configurationUpdated)
    }

    return fetch(`${baseUrl}/configuration`, options)
        .then((response) => { 
            if(response.status !== 200) {
                throw Error(response.status)
            }
            return response.json().then((data) => {
                return data;
            }).catch((err) => {
                console.log(err);
            }) 
        });
}

export const updateConfiguration = (configurationUpdated) => {
    const options = {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json;charset=UTF-8'
        },
        mode: 'cors',
        body: JSON.stringify(configurationUpdated)
    }

    return fetch(`${baseUrl}/configuration`, options)
        .then((response) => { 
            if(response.status !== 200) {
                throw Error(response.status)
            }
            return response.json().then((data) => {
                return data;
            }).catch((err) => {
                
                console.log(err);
                
            }) 
        });
}