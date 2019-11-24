import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { Row, Col } from 'reactstrap';
import { Status, AddDefaultSrc } from "../Helpers.js";
export class Home extends Component {
    static displayName = Home.name;

    constructor(props) {
        super(props);

        this.state = {
            movies: [],
            isLoaded: false,
            hasError: false,
        }
    }

    componentDidMount() {

        fetch('https://webjetmoviesapi20191124060522.azurewebsites.net/api/movies')
            .then(Status)
            .then(res =>  res.json())
            .then(json => {
                this.setState({
                    isLoaded: true,
                    movies: json,
                })

            })
            .catch(error => {
                this.setState({
                    isLoaded: true,
                    hasError: true
                })
            })
    }

    render() {
        var { isLoaded, movies, hasError } = this.state;

        if (!isLoaded) {
            return <h3 className="text-info">Fetching data from the API........</h3>
        }

        else {

            if (hasError) {
                return <h1 className="text-danger">No movies available from the APIs</h1>
            }
            else {

                return (
                    <div>


                        {movies.map(movie => (

                            <Row style={{ margin: '20px' }}>

                                <Col>
                                    <img src={movie.poster} onError={AddDefaultSrc} style={{ width: '50%' }} />
                                </Col>

                                <Col xs="6">
                                    <h5>{movie.title}</h5>
                                    <hr />
                                    <p><span class="font-weight-bold">Movie ID:</span> {movie.id}</p>
                                    <p><span class="font-weight-bold">Year</span> {movie.year}</p>
                                    <p><span class="font-weight-bold">Type:</span> {movie.type}</p>
                                </Col>

                                <Col className="text-center">

                                    <Link to={{
                                        pathname: `/movie/${movie.id}`,
                                        state: {
                                            movieID: movie.id
                                        }
                                    }}
                                        class="btn btn-primary">View Movie Prices</Link>
                                </Col>
                            </Row>
                        ))}

                    </div>
                );

            }


        }

      }

  }
