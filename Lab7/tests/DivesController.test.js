const http = require('http');
const logger = require('../config/logger'); 
const { getUserTokens } = require('./utils'); 
const request = require('supertest'); 

const PORT = 7142;
const BASE_PATH = '/api';

describe('Dives API Integration Tests', () => {
    let access_token;

    beforeAll(async () => {
        const tokens = await getUserTokens();
        access_token = tokens.access_token;
    });

    afterAll(() => {
        logger.info('Finished Dives API Integration Tests');
    });

    test('GET /dives with date range should return filtered dives', async () => {
        logger.info('Testing GET /dives with date range filter');

        const startDate = '2024-01-01';
        const endDate = '2024-01-31';

        const options = {
            hostname: 'localhost',
            port: PORT,
            path: `${BASE_PATH}/v1/dives?startDate=${startDate}&endDate=${endDate}`,
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
                logger.info(`Filtered dives: ${data}`);

                expect(res.statusCode).toBe(200);

                try {
                    const parsedData = JSON.parse(data);
                    expect(Array.isArray(parsedData)).toBe(true);

                    parsedData.forEach((dive) => {
                        const diveDate = new Date(dive.DiveDate);
                        expect(diveDate).toBeGreaterThanOrEqual(new Date(startDate));
                        expect(diveDate).toBeLessThanOrEqual(new Date(endDate));
                    });

                    logger.info(`Dives filtered by date successfully`);
                } catch (error) {
                    logger.error('Error parsing response data');
                }
            });
        });

        req.on('error', (error) => {
            logger.error(`Error testing GET /dives: ${error.message}`);
        });

        req.end();
    });
});
