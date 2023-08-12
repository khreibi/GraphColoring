# Graph Coloring Algorithm

## Introduction

Graph coloring is a classic NP-complete problem where the challenge is to color nodes of a graph such that no two adjacent nodes share the same color. This algorithm aims to solve the problem with certain optimizations to make it more efficient than traditional brute-force approaches.

## Techniques Used

### 1. Unique Permutation Coloring (UPC)

The UPC technique ensures that every color permutation is unique, avoiding redundant evaluations. Instead of trying every possible permutation of colors (which can grow exponentially with the number of nodes), UPC ensures that each permutation is tried only once. This significantly reduces the number of potential solutions the algorithm needs to check.

Example: 
Given a graph with four nodes, the colorings "1 1 2 3", "2 2 1 3", and "3 3 2 1" are essentially the same. UPC ensures that only one of these (e.g., "1 1 2 3") is considered.


The algorithm inherently ensures a unique sequence of colors for any valid graph coloring. This property, known as Unique Permutation Coloring (UPC), is achieved due to the sequential and iterative nature of the algorithm:

- **Sequential Coloring**: The algorithm colors vertices in a sequential manner, always processing the earlier vertices before the later ones.
- **Iterative Color Assignment**: For any given vertex, the algorithm iteratively assigns colors, always starting from the lowest available color. This ensures that if a color is available for a vertex, it gets assigned before considering the next color.
- **Consistent Color Sequence**: The recursive design of the algorithm ensures consistent color sequences. Once a vertex is colored, the algorithm doesn't alter its color unless there's a conflict with subsequent vertices.

Combined, these behaviors ensure that the algorithm naturally adheres to the UPC property, avoiding redundant evaluations of color sequences that represent the same valid coloring of the graph.

### 2. Biggest Possible Clique (BPC)

The BPC technique focuses on finding the biggest possible clique in the graph to set an upper bound. This helps in providing a more focused search space and allows the algorithm to bypass many non-viable solutions.

The idea behind BPC is rooted in graph theory: A graph that has a clique (a subset of vertices in which every two distinct vertices are adjacent) of size \(k\) requires at least \(k\) colors. Thus, by determining the size of the biggest possible clique in the graph, we can set a minimum bound for the number of colors required.


To determine an approximation of the biggest possible clique, we take into account the total number of edges in the graph. The rationale behind this is that a clique of size \( k \) will have exactly \( \binom{k}{2} \) edges. Therefore, if a graph has \( E \) edges, it can potentially contain a clique of size \( k \) where \( \binom{k}{2} \leq E \).

#### Formula:

Given a graph \( G \) with \( E \) edges, the maximum possible size \( k \) of a clique in \( G \) can be derived from the inequality:

\[ \binom{k}{2} \leq E \]

Where \( \binom{k}{2} \) represents the combination formula, which is equal to \( \frac{k(k-1)}{2} \).

#### Implication:

While this estimation doesn't guarantee the existence of such a clique in the graph, it provides a helpful heuristic for the chromatic number's lower bound. This is because, as mentioned earlier, a clique inherently demands that all its vertices be differently colored. Thus, by estimating the BPC, we set an initial lower threshold for the number of colors we'll need.

### How BPC Works in the Algorithm:

1. **Initialization**: Before the coloring process begins, the algorithm attempts to determine the size of the biggest possible clique in the graph. This is typically achieved using heuristic or approximation methods, as finding the maximum clique in a graph is an NP-hard problem in itself.
   
2. **Setting Color Lower Bound**: Once the size of the BPC is determined, this value serves as a lower bound for the number of colors. In other words, the algorithm doesn't need to attempt solutions with fewer colors than the size of the BPC, as they're guaranteed to be invalid.
   
3. **Synergy with Other Techniques**: When used in combination with other techniques, such as Neighbour Count Constraint (NCC), the BPC heuristic further refines the search space. For instance, if BPC determines that at least 4 colors are required and NCC for a certain vertex indicates a minimum of 5 colors, then the algorithm can start its search with 5 colors.

### Benefits:

- **Reduction in Search Space**: By providing a concrete lower bound on the number of colors, the BPC technique effectively reduces the number of invalid color configurations the algorithm needs to evaluate.
  
- **Efficiency**: Especially in graphs with large cliques, BPC can significantly speed up the coloring process by avoiding futile attempts to color the graph with too few colors.

### 3. Neighbour Count Constraint (NCC)

With the NCC technique, a node's maximum possible color is restricted by the number of its neighbours. If a node has three neighbours, then it can have a maximum color of four. This reduces the number of potential colors the algorithm has to check for each node.

### 4. Synergy of BPC and NCC 

The interplay between BPC and NCC can be best understood with a simple example:

- **Scenario 1** (Neighbors count > BPC): 
  Let's say the BPC of a graph is 4, implying that we need at least 4 colors for a valid coloring. Now, consider a node with 6 neighbors. For this node, due to the NCC, we would try coloring it with at most 7 colors (6 + 1). Thus, even though the BPC is 4, the NCC guides the algorithm to explore colors beyond the BPC for this specific node.

- **Scenario 2** (Neighbors count < BPC): 
  If the BPC is 6 and a specific node has just 3 neighbors, the algorithm will start with a base of 6 colors (from the BPC). However, for this particular node, NCC ensures that we only need to consider 4 of those colors (3 + 1).

This dynamic adaptation based on global (BPC) and local (NCC) constraints enhances the algorithm's efficiency, avoiding unnecessary explorations and thus optimizing the search space.

## Graph Representation

The graph in this algorithm is represented as an adjacency matrix. The first row and the first column denote the nodes of the graph. The intersection of a row and a column indicates whether an edge exists between the corresponding nodes.


## MatrixHelper.GenerateRandomConnectivityMatrix Explanation

The function `MatrixHelper.GenerateRandomConnectivityMatrix(n, p)` is designed to generate a random adjacency matrix for a graph with `n` nodes. The connectivity between nodes is determined by a probability `p`.

### Function Workflow:

1. **Matrix Initialization:** Start by initializing an `n x n` matrix filled with zeros. 
2. **Edge Determination:** For each pair of distinct nodes `i` and `j`, generate a random number between 0 and 1. 
   - If this number is less than `p`, it establishes an edge between nodes `i` and `j`. Thus, `matrix[i][j]` and `matrix[j][i]` are set to 1.
   - Otherwise, the entries remain 0, indicating no edge between the pair.
3. **Result:** The function returns the adjacency matrix representing the randomly generated graph.

**Note:** The generated matrix is symmetric about its diagonal, as the relationship between nodes is bidirectional.


## Main Function Explanation

The main function serves as the entry point of the program and primarily focuses on testing the graph coloring algorithm on various randomly generated graphs.

### Key Components:

1. **Initialization:**
   - `rand`: A random number generator.
   - `i`: A counter initialized to 0, tracking the number of graphs tested.
   - `matrix`: Represents the adjacency matrix of the graph.
   - `result`: Holds the result of the graph coloring algorithm.
   - `worstCase`: Tracks the worst-case scenario (likely in terms of computational effort or ticks).

2. **Testing Loop:**
   The main functionality is encapsulated in a `do-while` loop, which operates as follows:
   
   a. **Graph Generation:**
      - Increment the counter `i`.
      - Generate a random connectivity probability `p`.
      - Use `MatrixHelper.GenerateRandomConnectivityMatrix(1000, p)` to produce a random graph of 1000 nodes based on the connectivity probability `p`.
      
   b. **Graph Coloring:**
      - Instantiate a new `GraphColoring` object with the generated matrix.
      - Call the `ColorGraph()` method to color the graph. The result, after skipping the first color, is stored in `result`.
      - The coloring result is then printed to the console.
      
   c. **Tracking Worst Case:**
      - After each iteration, compare the current worst case from `GraphColoring.WorstCase` to the tracked `worstCase` variable. If the current is greater, update `worstCase`.
      - Reset `GraphColoring.WorstCase` to 0 for the next iteration.

   d. **Loop Continuation Condition:**
      - The loop continues as long as the coloring produced by the algorithm is valid, as determined by the `GraphColoring.IsValidColoring(matrix, result)` method.

**Note:** The main function is a test harness designed to evaluate the performance of the graph coloring algorithm on a variety of randomly generated graphs.


## Experimentation Results

Throughout our experimentation, we have tested the algorithm against randomly generated graphs with varying numbers of nodes and connectivity probabilities. One significant metric we have considered is the "worst-case scenario" based on computational effort or ticks. 

Below is a table summarizing the results:

| Nodes | Graphs Tested | Worst Case Ticks | Growth Factor |
|-------|---------------|------------------|---------------|
| 1000  | 10,000        | 500,500          | -             |
| 2000  | 1,000         | 1,275,992        | 2.55x         |
| 4000  | 500           | 4,719,367        | 3.70x         |


### Observations:

1. **Growth Factor**: As we can see from the table, the growth factor provides an indication of how the worst-case scenario scales as the number of nodes in the graph increases. The exact value might vary, but from the provided results, we notice that the growth is less than exponential, hinting towards polynomial growth.

2. **Variability with Connectivity**: The random generation of graphs means the connectivity probability (`p`) can vary. This variability can have an impact on the worst-case scenario, but from our extensive tests, the pattern remains consistent.

3. **Comparison with Existing Solutions**: When comparing our results to traditional algorithms, the performance of this algorithm showcases notable efficiency, especially for larger graphs.

### Limits:

While the developed algorithm has demonstrated impressive efficiency by solving graphs with up to 12,000 nodes in less than 2 minutes, we encountered a different challenge when scaling beyond this point. Rather than being constrained by computational time, we ran into a stack overflow issue.

### Conclusion:

The provided results strongly point towards polynomial growth, which is a significant observation given the nature of the graph coloring problem. This outcome makes me question the consistency and validity of the aggressive pruning logic implemented in the algorithm. While I've been unable to identify any apparent glitches in the approach, the efficiency achieved suggests there might be a flaw in the system. Therefore, I'm reaching out to the broader community to help identify any potential shortcomings or oversights in the algorithm.
