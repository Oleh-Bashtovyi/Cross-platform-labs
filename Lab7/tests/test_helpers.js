'use strict';

const axios = require('axios');
const qs = require('qs');

const DOMAIN = 'dev-8bvyc0fmlk4x5vyn.us.auth0.com';
const CLIENT_ID = 'pzd9IqmpDeCckbDByWYSBYJHix9muokE';
const CLIENT_SECRET = 'PHYs0taICQRNa1XQ90RRRIYcLYhH0_e-leb1A0wbDK6F9obZnv6Jxup1w6aJc3rn';
const AUDIENCE = `https://${DOMAIN}/api/v2/`;
const LOGIN = 'adminornot_444@gmail.com';
const PASSWORD = 'This!728neW0';

const getUserTokens = async () => {
    const options = {
        method: 'POST',
        url: `https://${DOMAIN}/oauth/token`,
        headers: { 'content-type': 'application/x-www-form-urlencoded' },
        data: qs.stringify({
            grant_type: 'password',
            username: LOGIN,
            password: PASSWORD,
            audience: AUDIENCE,
            scope: 'openid profile offline_access',
            client_id: CLIENT_ID,
            client_secret: CLIENT_SECRET
        })
    };

    try {
        const response = await axios(options);
        return response.data;
    } catch (error) {
        throw new Error('Invalid credentials or request failed');
    }
};

module.exports = { getUserTokens };