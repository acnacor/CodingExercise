import React, { Component } from 'react';
import { Col, Row, Table } from 'reactstrap';
import { Status, AddDefaultSrc } from "../Helpers.js";

export class Movie extends Component {
    static displayName = Movie.name;

    constructor(props) {
        super(props);

        this.state = {
            movies: {},
            isLoaded: false,
            hasError: false
        }
    }

    componentDidMount() {

        fetch(`https://webjetmoviesapi20191124060522.azurewebsites.net/api/movie/${this.props.match.params.id}`)
            .then(Status)
            .then(res => res.json())
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
            return <h3 className="text-info">Fetching movie details from the API.....</h3>
        }

        if (hasError) {
            return <h3 className="text-danger">The Movie ID: {this.props.match.params.id} does not exist....</h3>
        } else {

            return (
                <div>
                    <Row>
                        <Col>
                            <img src={movies['poster']} onError={AddDefaultSrc} style={{ width: '75%' }} />
                        </Col>

                        <Col xs="8">
                            <h1>{movies['title']}</h1>
                            <p className="text-justify">{movies['plot']}</p>

                            <p><span className="font-weight-bold">Director: </span>{movies['director']}</p>
                            <p><span className="font-weight-bold">Writer: </span>{movies['writer']}</p>
                            <p><span className="font-weight-bold">Actors: </span>{movies['actors']}</p>
                            <p><span className="font-weight-bold">Genre: </span>{movies['genre']}</p>
                            <p><span className="font-weight-bold">Rated: </span>{movies['rated']}</p>
                            <p><span className="font-weight-bold">Runtime: </span>{movies['runtime']}</p>
                        </Col>
                    </Row>


                    <Table hover style={{ margin: '50px' }} className="text-center" size="sm">
                        <thead>
                            <tr>
                                <th>Movie Providers</th>
                                <th>Prices</th>
                            </tr>
                        </thead>
                        <tbody>
                            {movies['prices'].map(movie => (
                                <tr>
                                    <td>{movie.providerName}</td>
                                    <td>{movie.price}</td>
                                </tr>
                            ))}
                        </tbody>
                    </Table>

                </div>
            );

        }



  }
}
