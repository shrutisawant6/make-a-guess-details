import React, { Component } from 'react';

export class FetchData extends Component {
    static displayName = FetchData.name;

    constructor(props) {
        super(props);
        this.state = {
            quiz: {},
            loading: true,
            countDown: 10,
        };
        this.timer = setInterval(() => this.tick(), props.timeout || 1000);
        this.constant = {
            gameOverCode: 99990
        };
    }

    componentDidMount() {
        this.populateData();
    }

    tick() {
        const current = this.state.countDown;
        if (current === 0) {
            this.transition();
        } else {
            this.setState({ countDown: current - 1 });
        }
    }

    transition() {
        clearInterval(this.timer);
    }

    populateNextData(currentQuestionId) {
        this.populateData(currentQuestionId);
        this.setState({ quiz: {}, loading: true, countDown: 10 });
        this.transition()
        this.timer = setInterval(() => this.tick(), 1000);
    }

    static renderQuizTable(quiz, countDown, gameOverCode) {
        return (
            <div>
                <div>
                    {quiz.id === gameOverCode &&
                        <h1 className="aliens">
                            {quiz.answer.map(ans =>
                                <span>{ans}</span>
                            )}
                        </h1>
                    }

                    <h2>
                        <div className="timer"><u>Count down</u> : {countDown}</div>
                        <u>Question</u> : {quiz.question}
                    </h2>
                    {countDown === 0 &&
                        <h1 className="aliens">
                            {quiz.answer.map(ans =>
                                <span>{ans}</span>
                            )}
                        </h1>
                    }
                </div>
            </div>
        );
    }

    static renderFinalDisplay(quiz, gameOverCode) {
        return (
            <div>
                <h2>
                    {quiz.question}
                </h2>
                {quiz.id === gameOverCode &&
                    <h1 className="aliens">
                        {quiz.answer.map(ans =>
                            <span>{ans}</span>
                        )}                       
                    </h1>
                }
                <label className="floatingText">How many did you get right ? <span>&#129300;</span></label>
            </div>
        );
    }

    render() {
        let contents = this.state.loading
            ? <div className="loadingDiv">Loading...</div>
            :
            (this.state.quiz.id === this.constant.gameOverCode ?
                FetchData.renderFinalDisplay(this.state.quiz, this.constant.gameOverCode) :
                FetchData.renderQuizTable(this.state.quiz, this.state.countDown, this.constant.gameOverCode));

        return (
            <div>
                {contents}
                <button className={this.state.quiz.id === this.constant.gameOverCode ? "buttonStyle hideData" : "buttonStyle"} onClick={(e) => this.populateNextData(this.state.quiz.id)}>
                    Next 
                </button>
            </div>
        );
    }

    async populateData(currentQuestionId) {
        const response = await fetch('quiz/GetResponse', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                id: currentQuestionId
            })
        });
        const data = await response.json();
        this.setState({ quiz: data, loading: false }); 
    }
}
