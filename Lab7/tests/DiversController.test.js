const http = require('http');
const logger = require('../config/logger');
const { getUserTokens } = require('./utils');
const request = require('supertest');

const PORT = 7142;
const BASE_PATH = '/api';

describe('Divers API Integration Tests', () => {
    let access_token;

    beforeAll(async () => {
        const tokens = await getUserTokens();
        access_token = tokens.access_token;
    });

    afterAll(() => {
        logger.info('Finished Divers API Integration Tests');
    });

    test('GET /divers Should return a list of divers', async () => {
        logger.info('Testing GET /divers endpoint');

        const options = {
            hostname: 'localhost',
            port: PORT,
            path: `${BASE_PATH}/v1/divers`,
            method: 'GET',
            headers: {
                Authorization: `Bearer ${access_token}`,
            },
            agent: new http.Agent({
                rejectUnauthorized: false
            }),
            timeout: 10000,
        };

        const req = http.request(options, (res) => {
            let data = '';

            res.on('data', (chunk) => {
                data += chunk;
            });

            res.on('end', () => {
                expect(res.statusCode).toBe(200);

                try {
                    const parsedData = JSON.parse(data);
                    expect(Array.isArray(parsedData)).toBe(true);
                } catch (error) {
                    logger.error('Error parsing response data');
                }
            });
        });

        req.on('error', (error) => {
            logger.error(`Error testing GET /Product: ${error.message}`);
        });

        req.end();
    });


    test('GET /divers/id should return a diver by ID', async () => {
        logger.info('Testing GET /diver/id endpoint');

        const diverId = 'f47ac10b-58cc-4372-a567-0e02b2c3d479';

        const options = {
            hostname: 'localhost',
            port: PORT,
            path: `${BASE_PATH}/v1/divers/${diverId}`,
            method: 'GET',
            headers: {
                Authorization: `Bearer ${access_token}`,
            },
            agent: new http.Agent({
                rejectUnauthorized: false,
            }),
            timeout: 10000,
        };

        const req = http.request(options, (res) => {
            let data = '';

            res.on('data', (chunk) => {
                data += chunk;
            });

            res.on('end', () => {
                logger.info(`Received product by ID: ${data}`);

                expect(res.statusCode).toBe(200);

                try {
                    const parsedData = JSON.parse(data);
                    expect(response.status).toBe(200); 
                    expect(parsedData).toHaveProperty('DiverId', diverId); 
                    expect(parsedData).toHaveProperty('DiverName'); 
                    expect(parsedData).toHaveProperty('DiverDetails');
                    logger.info(`Diver with ID ${diverId} fetched successfully`);
                } catch (error) {
                    logger.error('Error parsing response data');
                }
            });
        });

        req.on('error', (error) => {
            logger.error(`Error testing GET /Product/:id: ${error.message}`);
        });

        req.end();
    });
});
